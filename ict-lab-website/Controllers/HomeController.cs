﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ict_lab_website.Process;
using Newtonsoft.Json.Linq;
using ict_lab_website.Models.ViewModels;
using Microsoft.Extensions.Logging;
using ict_lab_website.Models.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using ict_lab_website.Models.Users;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace ict_lab_website.Controllers
{
    public class HomeController : Controller
    {
		private readonly IHomeCredentials _homecredentials;
        private readonly IUsers _users;
		private readonly ILogger _logger;

		public HomeController(IHomeCredentials homecredentials, IUsers users, ILogger<HomeController> logger){
			_homecredentials = homecredentials;
            _users = users;
			_logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.role = HttpContext.Session.GetString("Role");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Role");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login(string Succeed)
        {
            if(HttpContext.Session.GetString("Role") != null)
            {
                return RedirectToAction("AlreadyLoggedIn");
            }
            ViewBag.succeed = Succeed;
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

            if (succeed)
			{
				CookieOptions options = new CookieOptions();
				options.Expires = DateTime.Now.AddDays(1);
				Response.Cookies.Append(".AspNetCore.Identity.Application", UserObject.login);
			}

			if(succeed == false){
                return RedirectToAction("Login", new RouteValueDictionary(new { controller = "Home", action = "Login", Succeed = "failed" }));
            }
            return RedirectToAction("BetweenPage", new RouteValueDictionary(new { controller = "Home", action = "BetweenPage", Email = c.email }));
        }

        [HttpGet]
        public IActionResult Register(string Succeed)
        {
            if (HttpContext.Session.GetString("Role") != null)
            {
                return RedirectToAction("AlreadyLoggedIn");
            }

            ViewBag.succeed = Succeed;
            return View();
        }

        [HttpPost]
		public IActionResult Register(CredentialsViewModel c)
        {
            var checkInternetConnection = CheckInternetConnection.CheckConnection();

            if (checkInternetConnection == false)
            {
                ViewBag.internet = checkInternetConnection;
                return View();
            }

            var stringJson = JsonConvert.SerializeObject(c);
            var rJson = JObject.Parse(stringJson);
			var returntype = _homecredentials.RegisterCredentials(rJson);

            var succeed = returntype["Succeed"].Value<Boolean>();

            if (succeed == false)
            {
                return RedirectToAction("Register", new RouteValueDictionary(new { controller = "Home", action = "Register", Succeed = "failed" }));
            }

            return RedirectToAction("Login", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotAuthorized()
        {
            ViewBag.role = HttpContext.Session.GetString("Role");
            return View();
        }

        public IActionResult AlreadyLoggedIn()
        {
            ViewBag.role = HttpContext.Session.GetString("Role");
            return View();
        }

        public IActionResult BetweenPage(string Email)
        {
            var requestCookie = Request.Cookies[".AspNetCore.Identity.Application"];
            var checkRoleOfUser = _users.CheckRole(Email, requestCookie);

            var message = JObject.Parse(checkRoleOfUser)["RoleName"];
            var roleString = message.Value<string>();
            HttpContext.Session.SetString("Role", roleString);

            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Rooms", action = "Index" }));
        }
    }
}
