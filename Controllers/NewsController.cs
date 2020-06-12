using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beruwala_Mirror.Models.News;
using Beruwala_Mirror.Models.Users;
using Beruwala_Mirror.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Beruwala_Mirror.Controllers
{
    public class NewsController : Controller
    {
        private const string userFilePath = @"News/users.json";
        private const string newsItems = @"News/NewsItems";
        
        private readonly IFileUploader _fileUploader;
        private readonly ICacheManager _cacheManager;

        // GET: News

        public NewsController(IFileUploader fileUploader, ICacheManager cacheManager)
        {
            _fileUploader = fileUploader;
            _cacheManager = cacheManager;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        // GET: News/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var model = await GetNews(id);

            ViewBag.LoggedIn = false;
            if (HttpContext.Session.GetString("Name") != null)
            {
                ViewBag.LoggedIn = true;
            }
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
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var email = collection["EmailAddress"];
                var password = collection["Password"];
                var user = await IsValidUser(email, password);

                if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase)) HttpContext.Session.SetString("Name", user.Name);

                return RedirectToAction("News", "Admin");
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

        private async Task<UserModel> IsValidUser(string email, string password)
        {
            var json = await _fileUploader.GetFileFromS3(userFilePath);
            var users = JsonConvert.DeserializeObject<AdminUsers>(json);

            if (users.Users.Any(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase) && string.Equals(u.Password, password, StringComparison.CurrentCultureIgnoreCase)))
                return users.Users.First(
                    u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase) && string.Equals(u.Password, password, StringComparison.CurrentCultureIgnoreCase));

            return new UserModel();
        }

        private async Task<NewsModel> GetNews(string newsId)
        {
            var model = new NewsModel();
            try
            {
                var newsModel = _cacheManager.Get<List<NewsModel>>("NewsItems").FirstOrDefault(n => n.Id == newsId);

               // var responseBody =  await _fileUploader.GetFileFromS3(@"News/NewsItems/" + newsId + ".json");
              //   var newsModel = JsonConvert.DeserializeObject<NewsModel>(responseBody);
                newsModel.MainImg = newsModel.Images.FirstOrDefault();
                model = newsModel;
               var update = await UpdateVisits(model);

            }
            catch (Exception ex)
            {
                //ignore 
            }

            return model;
        }

        private async Task<bool> UpdateVisits(NewsModel model)
        {
            model.NumberOfVisits++;
            var jsonString = JsonSerializer.Serialize(model);

            var isSaved = await _fileUploader.SaveFileAsync($@"{newsItems}/{model.Id + ".json"}", jsonString)
                .ConfigureAwait(false);

            return true;

        }
    }
}