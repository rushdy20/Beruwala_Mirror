using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beruwala_Mirror.Models.Admin;
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Beruwala_Mirror.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFileUploader _fileUploader;
        private const string newsPath = @"News";
        private const string newsItems = @"News/NewsItems";

        public AdminController(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> News(string id)
        {
            if (HttpContext.Session.GetString("Name") == null)
                return RedirectToAction("Create", "News");

            var model = new CreateNewsViewModel
            {
                Id = Guid.NewGuid().ToString(),
                CreatedBy = HttpContext.Session.GetString("Name"),
                CreatedDate = DateTime.Today.ToShortDateString()
            };

            if (!string.IsNullOrEmpty(id))
            {
                model = await getNewsModel(id);
            }

            
            return View(model);
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
                YouTubLink = model.YouTubLink ?? string.Empty
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

            return RedirectToAction("index", "Home");
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
                var responseBody = await _fileUploader.GetFileFromS3(@"News/NewsItems/" + Id + ".json");
                var newsModel = JsonConvert.DeserializeObject<CreateNewsViewModel>(responseBody);
                
                return newsModel;
            }
            catch (Exception ex)
            {
                //ignore 
            }

            return model;
        }
    }
}