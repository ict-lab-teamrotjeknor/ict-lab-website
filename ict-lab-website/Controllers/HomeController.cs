using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Models;
using Newtonsoft.Json;
using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Routing;

namespace ict_lab_website.Controllers
{
    public class HomeController : Controller
    {
        private ApiCalls _apiCalls;  

        public HomeController(){
            _apiCalls = new ApiCalls();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel l)
        {
            var stringJson = JsonConvert.SerializeObject(l);
            var rJson = JObject.Parse(stringJson);

            var returnType = _apiCalls.PostRequest(rJson, "http://145.24.222.103:8080/authentication/signin");

            return RedirectToAction("Index", "Rooms");   
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel r)
        {
            var stringJson = JsonConvert.SerializeObject(r);
            var rJson = JObject.Parse(stringJson);

            var returnType = _apiCalls.PostRequest(rJson, "http://145.24.222.103:8080/authentication/signup");

            return RedirectToAction("Index", "Rooms");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
