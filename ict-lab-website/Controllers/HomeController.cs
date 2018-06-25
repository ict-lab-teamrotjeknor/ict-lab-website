using System;
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
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("Role") != null)
            {
                return RedirectToAction("AlreadyLoggedIn");
            }
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
				ViewBag.succeed = succeed;
				return View();
			}

            //var checkRoleOfUser = _users.CheckRole(c.email);

            if(c.email == "admin@admin.nl")
            {
                HttpContext.Session.SetString("Role", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("Role", "Student");
            }
           
			return RedirectToAction("Index", "Rooms");
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
                return RedirectToAction("Register", new RouteValueDictionary(new { controller = "Home", action = "Register", Succeed = "success" }));
            }

            if (c.email == "admin@admin.nl")
            {
                HttpContext.Session.SetString("Role", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("Role", "Student");
            }

            return RedirectToAction("Index", "Rooms");
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
    }
}
