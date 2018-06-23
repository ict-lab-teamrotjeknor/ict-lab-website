using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Models.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                List<Roles> rList = new List<Roles>();
                rList.Add(new Roles() { RoleId = 1, RoleName = "Guest", IsChecked = false });
                rList.Add(new Roles() { RoleId = 2, RoleName = "Student", IsChecked = true });
                rList.Add(new Roles() { RoleId = 3, RoleName = "Teacher", IsChecked = false });
                rList.Add(new Roles() { RoleId = 4, RoleName = "Handyman", IsChecked = false });
                rList.Add(new Roles() { RoleId = 5, RoleName = "Servicedesk", IsChecked = false });
                rList.Add(new Roles() { RoleId = 6, RoleName = "Rastermaker", IsChecked = false });
                rList.Add(new Roles() { RoleId = 7, RoleName = "Administrator", IsChecked = false });

                RoleList roleList = new RoleList();
                roleList.roles = rList;

                var returnType = _users.GetAllUsers();
                if (returnType == null)
                {
                    ViewBag.returnType = "error";
                    return View();
                }
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
                DataTable dataTable = dataSet.Tables["Users"];

                UsersViewModel viewModel = new UsersViewModel(dataTable, roleList);

                ViewBag.role = HttpContext.Session.GetString("Role");

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

			var returnType = _users.ChangeRoleOfUser(jsonObject);

			return RedirectToAction("Index");
		}
        
		[HttpPost]
		public IActionResult DeleteUser(string email)
		{
			DeleteUser user = new DeleteUser(email);

			var stringJson = JsonConvert.SerializeObject(user);
            var jsonObject = JObject.Parse(stringJson);
            
			var returnType = _users.DeleteAnUser(jsonObject);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult AddRole(string RoleName)
		{
			AddNewRole newRole = new AddNewRole(RoleName);
            
			var stringJson = JsonConvert.SerializeObject(newRole);
            var jsonObject = JObject.Parse(stringJson);

			var returnType = _users.AddRole(jsonObject);

			return RedirectToAction("Index");
		}
    }
}