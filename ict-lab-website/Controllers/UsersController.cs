using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Models.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Routing;

namespace ictlabwebsite.Controllers
{
    public class UsersController : Controller
    {
		private readonly IUsers _users;
        private readonly ILogger _logger;

		public UsersController(IUsers users ,ILogger<UsersController> logger)
        {
			_users = users;
			_logger = logger;
        }
        
        [HttpGet]
		public IActionResult Index(string Succeed)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                var requestCookie = Request.Cookies[".AspNetCore.Identity.Application"];
                var roles = _users.GetRoles(requestCookie);

                AllRoles allroles = JsonConvert.DeserializeObject<AllRoles>(roles);
                List<Roles> rList = new List<Roles>();

                for (int i = 0; i < allroles.random.Count; i++)
                {
                    if(i == 0)
                    {
                        rList.Add(new Roles() { RoleId = i, RoleName = allroles.random[i], IsChecked = true });
                    } else
                    {
                        rList.Add(new Roles() { RoleId = i, RoleName = allroles.random[i], IsChecked = false });
                    }
                }

                RoleList roleList = new RoleList();
                roleList.roles = rList;

                var returnType = _users.GetAllUsers();
                if (returnType == null)
                {
                    ViewBag.role = HttpContext.Session.GetString("Role");
                    ViewBag.returnType = "error";
                    return View();
                }
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
                DataTable dataTable = dataSet.Tables["Users"];

                UsersViewModel viewModel = new UsersViewModel(dataTable, roleList);

                ViewBag.role = HttpContext.Session.GetString("Role");
				ViewBag.Succeed = Succeed;

                return View(viewModel);
            } else {
                return RedirectToAction("NotAuthorized", "Home");
            }
        }
        
		[HttpPost]
		public IActionResult ChangeRole(RoleList _roleList, string _email)
		{
			List<string> roleStr = new List<string>();
			foreach(var item in _roleList.roles){
				if(item.IsChecked){
					var tester = item.RoleName;
					roleStr.Add(tester);
				}
			}
            
			ChangeUserRole changeuserrole = new ChangeUserRole(_email, roleStr[0]);
            
			var stringJson = JsonConvert.SerializeObject(changeuserrole);
			var jsonObject = JObject.Parse(stringJson);

			var requestCookie = Request.Cookies[".AspNetCore.Identity.Application"];

			var returnType = _users.ChangeRoleOfUser(jsonObject, requestCookie);

			var succeed = returnType["Succeed"].Value<Boolean>();

            if (succeed)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Users", action = "Index", Succeed = "success" }));
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Users", action = "Index", Succeed = "error" }));
		}
        
		[HttpPost]
		public IActionResult DeleteUser(string email)
		{
			DeleteUser user = new DeleteUser(email);

			var stringJson = JsonConvert.SerializeObject(user);
            var jsonObject = JObject.Parse(stringJson);

			var requestCookie = Request.Cookies[".AspNetCore.Identity.Application"];
            
			var returnType = _users.DeleteAnUser(jsonObject, requestCookie);

			var succeed = returnType["Succeed"].Value<Boolean>();

			if (succeed)
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Users", action = "Index", Succeed = "success" }));
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Users", action = "Index", Succeed = "error" }));
		}

		[HttpPost]
		public IActionResult AddRole(string RoleName)
		{
			AddNewRole newRole = new AddNewRole(RoleName);
            
			var stringJson = JsonConvert.SerializeObject(newRole);
            var jsonObject = JObject.Parse(stringJson);

			var requestCookie = Request.Cookies[".AspNetCore.Identity.Application"];

			var returnType = _users.AddRole(jsonObject, requestCookie);

			var succeed = returnType["Succeed"].Value<Boolean>();
            
			if(succeed){
				return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Users", action = "Index", Succeed = "success" }));
			}
			return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Users", action = "Index", Succeed = "error" }));
		}
    }
}