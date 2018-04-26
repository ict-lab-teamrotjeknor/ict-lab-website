using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Process;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Models.Users;
using Newtonsoft.Json;

namespace ictlabwebsite.Controllers
{
    public class UsersController : Controller
    {
        private ApiCalls _apiCalls;

        public UsersController()
        {
            _apiCalls = new ApiCalls();
        }

        public IActionResult Index(List<string> Id, List<string> Email)
        {
            var model = new UsersViewModel(Id, Email);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var returnType = _apiCalls.GetRequest("http://145.24.222.103:8080/manage/getusers");
            dynamic newReturnType = JsonConvert.DeserializeObject(returnType);

            List<string> Id = new List<string>();
            List<string> Email = new List<string>();

            foreach(var item in newReturnType)
            {
                var test = item["Id"];
            }

            return RedirectToAction("Index", new { Id = Id, Email = Email });
        }
    }
}