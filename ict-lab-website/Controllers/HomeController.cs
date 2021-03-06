﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using ict_lab_website.Models.ViewModels;
using Microsoft.Extensions.Logging;
using ict_lab_website.Models.Home;

namespace ict_lab_website.Controllers
{
    public class HomeController : Controller
    {
		private readonly IHomeCredentials _homecredentials;
		private readonly ILogger _logger;

		public HomeController(IHomeCredentials homecredentials, ILogger<HomeController> logger){
			_homecredentials = homecredentials;
			_logger = logger;
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
		public IActionResult Login(CredentialsViewModel c)
        {
			var checkInternetConnection = CheckInternetConnection.CheckConnection();

			if(checkInternetConnection == false){
				ViewBag.internet = checkInternetConnection;
				return View();
			}

            var stringJson = JsonConvert.SerializeObject(c);
            var rJson = JObject.Parse(stringJson);
			var returntype = _homecredentials.LoginCredentials(rJson);
            
			var succeed = returntype["Succeed"].Value<Boolean>();

			if(succeed == false){
				ViewBag.succeed = succeed;
				return View();
			}
           
			return RedirectToAction("Index", "Rooms");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Register(CredentialsViewModel c)
        {
            var stringJson = JsonConvert.SerializeObject(c);
            var rJson = JObject.Parse(stringJson);
			var returntype = _homecredentials.RegisterCredentials(rJson);

			//var succeed = returntype["Succeed"].Value<Boolean>();

			//if (succeed == false)
            //{
            //    return RedirectToAction("Register");
            //}

            return RedirectToAction("Index", "Rooms");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
