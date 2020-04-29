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
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, IFileUploader fileUploader)
        {
            _logger = logger;
            _s3Client = new AmazonS3Client(bucketRegion);
            _fileUploader = fileUploader;
        }

        public async Task<IActionResult> Index()
        {
            var newsFromS3 = await GetNewsFromS3();
            var model = new NewsViewModel
            {
                News = newsFromS3
            };
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
            var newsCollection = new List<NewsModel>();
            try
            {
                var req = new ListObjectsV2Request
                {
                    BucketName = BucketName,
                    Prefix = "News/NewsItems"
                };
                
                var listing = await _s3Client.ListObjectsV2Async(req);
                foreach (var s3 in listing.S3Objects)
                {
                    var responseBody = await _fileUploader.GetFileFromS3(s3.Key);
                    var newsModel = JsonConvert.DeserializeObject<NewsModel>(responseBody);
                    newsModel.MainImg = newsModel.Images.FirstOrDefault();
                    newsModel.NewsBody = newsModel.NewsBody.Length > 100 ? newsModel.NewsBody.Substring(0, 99) : newsModel.NewsBody;
                    newsCollection.Add(newsModel);
                }
            }
            catch (Exception ex)
            {
                //ignore 
            }

            return newsCollection.OrderByDescending(n => n.CreatedDate).ToList();
        }

        public ActionResult Thumbnail(string fileName)
        {
            return Redirect($"{CloudFront}/{fileName}");
        }
    }
}