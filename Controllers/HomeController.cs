using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Beruwala_Mirror.Models;
using Beruwala_Mirror.Models.News;

namespace Beruwala_Mirror.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new NewsViewModel
            {
                News = new List<NewsModel>
                {
                    new NewsModel {Heading = "heading-1", NewsBody = "adsf adsfas asdf ads asdfasd asdf  asdfasdf asdfasd asdfdas  asdfasdfadsfadsasdf", Id="abc"},
                    new NewsModel {Heading = "heading-2", NewsBody = "adsf adsfas asdf ads asdfasd asdf  asdfasdf asdfasd asdfdas  asdfasdfadsfadsasdf", Id="abc"},
                    new NewsModel {Heading = "heading-3", NewsBody = "adsf adsfas asdf ads asdfasd asdf  asdfasdf asdfasd asdfdas  asdfasdfadsfadsasdf", Id="abc"},
                    new NewsModel {Heading = "heading-4", NewsBody = "adsf adsfas asdf ads asdfasd asdf  asdfasdf asdfasd asdfdas  asdfasdfadsfadsasdf", Id="abc"},
                    new NewsModel {Heading = "heading-5", NewsBody = "adsf adsfas asdf ads asdfasd asdf  asdfasdf asdfasd asdfdas  asdfasdfadsfadsasdf", Id="abc"}
                }
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
