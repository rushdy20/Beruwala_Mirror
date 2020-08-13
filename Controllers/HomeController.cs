using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Beruwala_Mirror.Models;
using Beruwala_Mirror.Models.Admin;
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Beruwala_Mirror.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string CloudFront = "http://d1j1ye7fpazrcq.cloudfront.net";
        private const string BucketName = "beruwalamirror";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 _s3Client;
        private readonly IFileUploader _fileUploader;
        private readonly IWatsAppMessageServices _watsAppMessageServices;
        private readonly ICacheManager _cache;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, IFileUploader fileUploader, IWatsAppMessageServices watsAppMessageServices, ICacheManager cacheManager)
        {
            _logger = logger;
            _s3Client = new AmazonS3Client(bucketRegion);
            _fileUploader = fileUploader;
            _watsAppMessageServices = watsAppMessageServices;
            _cache = cacheManager;

            var a = GetNewsCategories().Result;

           // ViewData["newsCategory"] = a;
           ViewBag.NewsCategory = a;
        }

        public async Task<IActionResult> Index(int cat)
        {
          //  var newsFromS3 = new NewsModel[] {new NewsModel {CategoryId = 1, CreatedDate = DateTime.Today, DisplayDate = DateTime.Today,Images = new List<string>(), NewsBody = string.Empty, YouTubLink ="https://"} };
           var newsFromS3 = await GetNewsFromS3();
            var model = new NewsViewModel
            {
                News = cat >0 ?  newsFromS3.Where(n => n.CategoryId == cat) : newsFromS3
            };

           // _watsAppMessageServices.SendMessage("Hi","7885860529");
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        private async Task<List<NewsModel>> GetNewsFromS3()
        {
            var newsFromCache = _cache.Get<List<NewsModel>>("NewsItems");
            if (newsFromCache !=null && newsFromCache.Any()) return newsFromCache;

            var newsItemsFromFile = await _fileUploader.GetFileFromS3(@"News/NewsItems.json"); // _fileUploader.ReadFile("NewsItems.json");

            if (!string.IsNullOrWhiteSpace(newsItemsFromFile))
            {
              //  await _fileUploader.UpdateS3ReaderCount(true);
                var newsModel = JsonConvert.DeserializeObject<List<NewsModel>>(newsItemsFromFile);
                var newsItemsFile = newsModel.OrderByDescending(n => n.DisplayDate).ToList();
                _cache.Set<List<NewsModel>>("NewsItems", newsItemsFile);
                return newsItemsFile;
            }

            var newsCollection = new List<NewsModel>();
            try
            {
                var req = new ListObjectsV2Request
                {
                    BucketName = BucketName,
                    Prefix = "News/NewsItems"
                };
                
                
                var listing = await _s3Client.ListObjectsV2Async(req);
             //   await _fileUploader.UpdateS3ReaderCount(false);
                foreach (var s3 in listing.S3Objects)
                {
                    var responseBody = await _fileUploader.GetFileFromS3(s3.Key);
                    if (string.IsNullOrEmpty(responseBody)) continue;
                    var newsModel = JsonConvert.DeserializeObject<NewsModel>(responseBody);
                    newsModel.MainImg = newsModel.Images.FirstOrDefault();
                    newsModel.DisplayDate = newsModel.CreatedDate.AddDays(newsModel.TopNewsForDays);

                    if (newsModel.DisplayDate >= DateTime.Today)
                    {
                        newsModel.CreatedDate = DateTime.Today;
                    }
                    else
                    {
                        newsModel.DisplayDate = newsModel.CreatedDate;
                    }

                    newsCollection.Add(newsModel);
                }
            }
            catch (Exception ex)
            {
                //ignore 
            }

            var newsItems = newsCollection.OrderByDescending(n => n.DisplayDate).ToList();
               _cache.Set<List<NewsModel>>("NewsItems", newsItems);

              await _fileUploader.SaveFileAsync("News/NewsItems.json", JsonConvert.SerializeObject(newsItems));
               return _cache.Get<List<NewsModel>>("NewsItems");
        }

        public ActionResult Thumbnail(string fileName)
        {
            return Redirect(fileName.EndsWith("thumbnail.jfif") ? $"{CloudFront}/News/thumbnail.jfif" : $"{CloudFront}/{fileName}");
        }

        private async  Task<List<SelectListItem>> GetNewsCategories()
        {

            try
            {
                var cacheCategory =  _cache.Get<NewsCategories>("NewsCategories");
                if (cacheCategory != null)
                {
                    return cacheCategory.Categories.Select(s => new SelectListItem { Text = s.Name, Value = s.CategoryId.ToString() }).ToList();
                    
                }

                // var newsCategories = await _fileUploader.GetFileFromS3(@"News/NewsCategories.json");
                var newsCategories = _fileUploader.ReadFile("NewsCategories.json");
                if (newsCategories == null)
                {
                    return new List<SelectListItem>();
                }

                var categoriesFromS3 = JsonConvert.DeserializeObject<NewsCategories>(newsCategories);
                categoriesFromS3.Categories = categoriesFromS3.Categories.OrderBy(c => c.Order).ToList();
                _cache.Set("NewsCategories", categoriesFromS3);

                return categoriesFromS3.Categories.Select(s => new SelectListItem {Text = s.Name, Value = s.CategoryId.ToString()}).ToList();
            }
            catch (Exception ex)
            {
                //ignore 
            }
            
            return new List<SelectListItem>();
        }

        public List<SelectListItem> NewsCategory()
        {
            var model =  GetNewsCategories().Result;
            return model;

            // return View("_newscategory", model);

        }
    }
}