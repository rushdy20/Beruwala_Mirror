using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Beruwala_Mirror.Models.Admin;
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Newtonsoft.Json.JsonConvert;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Beruwala_Mirror.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFileUploader _fileUploader;
        private const string newsPath = @"News";
        private const string newsItems = @"News/NewsItems";
        private readonly ICacheManager _cacheManager;
        private NewsCategories _newsCategories;
        private Random _random;



        public AdminController(IFileUploader fileUploader, ICacheManager cacheManager)
        {
            _fileUploader = fileUploader;
            _cacheManager = cacheManager;
            _newsCategories = GetNewsCategories().Result;
            _random = new Random();
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            if (HttpContext.Session.GetString("Name") == null)
                return RedirectToAction("Create", "News");

            var model = new AdminModel();
            model.NewsCategories = await GetNewsCategories();
            return View(model);
        }

        // GET: Admin/Details/5
        public ActionResult Save()
        {
            UpdateCategoryInS3(_newsCategories);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> News(string id)
        {
            if (HttpContext.Session.GetString("Name") == null)
                return RedirectToAction("Create", "News");

            var model = new CreateNewsViewModel
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = HttpContext.Session.GetString("Name"),
                CreatedDate = DateTime.Today.ToShortDateString(),
                NewsCategories = _newsCategories.Categories.Select(s => new SelectListItem{ Text = s.Name, Value = s.CategoryId.ToString()}).ToList()
            };

            if (!string.IsNullOrEmpty(id))
            {
                model = await getNewsModel(id);
            }

            
            return View(model);
        }

        public async Task<ActionResult> Refresh()
        {
            _cacheManager.RemoveCache("NewsItems");
           await _fileUploader.SaveFileAsync(@"News/NewsItems.json", string.Empty);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> News(CreateNewsViewModel model)
        {
            if (string.IsNullOrEmpty(model.Heading))
                RedirectToAction("News");

            var newsId = model.Id;
            var folderPath = CreateFolder(newsId);
            var newsmodel = new NewsModel
            {
                Id = newsId,
                CreatedBy = HttpContext.Session.GetString("Name"),
                CreatedDate = DateTime.Today,
                Heading = model.Heading,
                NewsBody = model.NewsBody,
                YouTubLink = model.YouTubLink ?? string.Empty,
                CategoryId = int.Parse(model.CategoryId)
                
            };
            var stringImages = new List<string>();

            if (model.MainImage != null)
            {
                stringImages.Add(model.MainImage.FileName);
                using (var readStream = model.MainImage.OpenReadStream())
                {
                    var result = await _fileUploader.UploadFileAsync($@"{folderPath}/{model.MainImage.FileName}", readStream)
                        .ConfigureAwait(false);

                    if (!result)
                        throw new Exception($"Could not upload the image to file repository. Please see the logs for details.");
                }
            }
            else
            {
                stringImages.Add("thumbnail.jfif");
            }

            if (model.SubImage1 != null)
            {
                stringImages.Add(model.SubImage1.FileName);
                using (var readStream = model.SubImage1.OpenReadStream())
                {
                    var result = await _fileUploader.UploadFileAsync($@"{folderPath}/{model.SubImage1.FileName}", readStream)
                        .ConfigureAwait(false);

                    if (!result)
                        throw new Exception($"Could not upload the image to file repository. Please see the logs for details.");
                }
            }

            if (model.SubImage2 != null)
            {
                stringImages.Add(model.SubImage2.FileName);

                using (var readStream = model.SubImage2.OpenReadStream())
                {
                    var result = await _fileUploader.UploadFileAsync($@"{folderPath}/{model.SubImage2.FileName}", readStream)
                        .ConfigureAwait(false);

                    if (!result)
                        throw new Exception("Could not upload the image to file repository. Please see the logs for details.");
                }
            }

            if (model.SubImage3 != null)
            {
                stringImages.Add(model.SubImage3.FileName);

                using (var readStream = model.SubImage3.OpenReadStream())
                {
                    var result = await _fileUploader.UploadFileAsync($@"{folderPath}/{model.SubImage3.FileName}", readStream)
                        .ConfigureAwait(false);

                    if (!result)
                        throw new Exception("Could not upload the image to file repository. Please see the logs for details.");
                }
            }
             newsmodel.Images = stringImages;

            var jsonString = JsonSerializer.Serialize(newsmodel);

            var isSaved = await _fileUploader.SaveFileAsync($@"{newsItems}/{newsmodel.Id + ".json"}", jsonString)
                .ConfigureAwait(false);

            if (!isSaved)
                throw new Exception("Could not upload the image to file repository. Please see the logs for details.");

           // _cacheManager.RemoveCache("NewsItems");
           _cacheManager.SetNewsItem(newsmodel);

            return RedirectToAction("index", "Home");
        }


        [HttpPost]
        public async Task<ActionResult> AddCategory(IFormCollection collection)
        {

            var newCategory = new CategoryModel
            {
                Name = collection["AddCategory.Name"],
                CategoryId = _random.Next(1001,9999) 
            };
             _newsCategories.Categories.Add(newCategory);
            _cacheManager.Set("NewsCategories", _newsCategories);

            return RedirectToAction("Index");
        }


       public async Task<ActionResult>  Advertisement()
        {
            var model = new AdvertiseModel
            {
                Id = Guid.NewGuid().ToString(),
                StartDate = DateTime.Today
            };

            return View(model);
        }

        [HttpPost]

        public async Task<ActionResult> SaveAdvertisement(AdvertiseModel model)
        {
            if (model.MainImage == null)
                return View("Advertisement");

           using (var readStream = model.MainImage.OpenReadStream())
              {
                    var result = await _fileUploader.UploadFileAsync($@"Advertisement/{model.MainImage.FileName}", readStream)
                        .ConfigureAwait(false);

                    if (!result)
                        throw new Exception($"Could not upload the image to file repository. Please see the logs for details.");
             }

           var advertisements = new List<AdvertiseModel>();
            var advertisementsJson = await _fileUploader.GetFileFromS3(@"Advertisement/Advertisements.json");

            if (!string.IsNullOrEmpty(advertisementsJson))
            {
                advertisements = DeserializeObject<List<AdvertiseModel>>(advertisementsJson);
            }

            model.FileName = model.MainImage.FileName;
            model.MainImage = null;

            advertisements.Add(model);

            var jsonString = JsonSerializer.Serialize(advertisements);

            await _fileUploader.SaveFileAsync($@"Advertisement/Advertisements.json", jsonString);



            return RedirectToAction("Index", "Home");
        }
        

        private string CreateFolder(string newsId)
        {
            var dateFolder = DateTime.Today.ToString("dd-MM-yyy");
            return $@"{newsPath}/{dateFolder}/{newsId}";
        }

        private async Task<CreateNewsViewModel> getNewsModel(string Id)
        {

            var model = new CreateNewsViewModel();
            try
            {
                var newsFromCache = _cacheManager.Get<List<NewsModel>>("NewsItems").FirstOrDefault(n => n.Id == Id);

                if (newsFromCache != null)
                {
                    var json = SerializeObject(newsFromCache);
                    model = DeserializeObject<CreateNewsViewModel>(json);
                }
                else
                {
                    var responseBody = await _fileUploader.GetFileFromS3(@"News/NewsItems/" + Id + ".json");
                    model = DeserializeObject<CreateNewsViewModel>(responseBody);
                }

                var newsCategories = await GetNewsCategories();
                model.NewsCategories = newsCategories.Categories.Select(s => new SelectListItem{Text = s.Name, Value = s.CategoryId.ToString()}).ToList();
                
                return model;
            }
            catch (Exception ex)
            {
                //ignore 
            }

            return model;
        }

        private async Task<NewsCategories> GetNewsCategories()
        {
           
            try
            {
                var cachedCategories = _cacheManager.Get<NewsCategories>("NewsCategories");

                if (cachedCategories !=null && cachedCategories.Categories != null && cachedCategories.Categories.Any()) return cachedCategories;

                var newsCategories =  await _fileUploader.GetFileFromS3(@"News/NewsCategories.json");
                if (newsCategories ==  null)
                {
                    return new NewsCategories();
                }

                var categoriesFromS3 = DeserializeObject<NewsCategories>(newsCategories);
                _cacheManager.Set("NewsCategories", categoriesFromS3);
                _newsCategories = categoriesFromS3;
                return categoriesFromS3;

            }
            catch (Exception ex)
            {
                //ignore 
            }

            return null;
        }

        private async Task<bool> UpdateCategoryInS3(NewsCategories model)
        {
            
            var jsonString = JsonSerializer.Serialize(model);

            var isSaved = await _fileUploader.SaveFileAsync(@"News/NewsCategories.json", jsonString)
                .ConfigureAwait(false);

            return true;

        }

        private void UpdateCechNewsItem(string newsItem, string Id)
        {
            var newsFromCache = _cacheManager.Get<List<NewsModel>>("NewsItems").FirstOrDefault(n => n.Id == Id);
        }


    }
}