using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Beruwala_Mirror.Controllers
{
    public class NewsController : Controller
    {
        private const string userFilePath = @"\Imgs\users.json";
        private readonly IWebHostEnvironment _env;
        // GET: News

        public NewsController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public ActionResult Index()
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

        // GET: News/Details/5
        public ActionResult Details(string id)
        {
            var model = new NewsModel
            {
                Heading = "heading-1",
                NewsBody = "adsf adsfas asdf ads asdfasd asdf  asdfasdf asdfasd asdfdas  asdfasdfadsfadsasdf",
                MainImg = "sample.jfif",
                Images = new List<string> {"sample.jfif", "sample.jfif", "sample.jfif"}
            };
            return View(model);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            var model = new LoginModel();

            return View(model);
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var email = collection["EmailAddress"];
                var password = collection["Password"];
                var user = IsValidUser(email, password);

                if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
                {
                   HttpContext.Session.SetString("Name",user.Name);
                }
                
                return RedirectToAction("News","Admin");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: News/Edit/5
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

        // GET: News/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: News/Delete/5
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

        private UserModel IsValidUser(string email, string password)
        {
            string webRootPath = _env.WebRootPath;
            string json =System.IO. File.ReadAllText(webRootPath + userFilePath);
            var  users = JsonConvert.DeserializeObject<AdminUsers>(json);

            if (users.Users.Any(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase) &&  string.Equals(u.Password, password, StringComparison.CurrentCultureIgnoreCase)))
            {
                return users.Users.First(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase) && string.Equals(u.Password, password, StringComparison.CurrentCultureIgnoreCase));
            }
            
            return new UserModel();
        }
    }
}