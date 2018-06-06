using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Process;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Models.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;
using Microsoft.Extensions.Logging;
using ict_lab_website.Models.Home;

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
			List<Roles> rList = new List<Roles>();
			rList.Add(new Roles() { RoleId = 1, RoleName = "Guest", IsChecked = false });
			rList.Add(new Roles() { RoleId = 2, RoleName = "Student", IsChecked = false });
			rList.Add(new Roles() { RoleId = 3, RoleName = "Teacher", IsChecked = false });
			rList.Add(new Roles() { RoleId = 4, RoleName = "Handyman", IsChecked = true });
			rList.Add(new Roles() { RoleId = 5, RoleName = "Servicedesk", IsChecked = false });
			rList.Add(new Roles() { RoleId = 6, RoleName = "Rastermaker", IsChecked = false });
			rList.Add(new Roles() { RoleId = 7, RoleName = "Administrator", IsChecked = false });
            
			RoleList roleList = new RoleList();
			roleList.roles = rList;

			var returnType = _users.GetAllUsers();
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
            DataTable dataTable = dataSet.Tables["Users"];

			UsersViewModel viewModel = new UsersViewModel(dataTable, roleList);
            
            return View(viewModel);
        }
        
		[HttpPost]
		public IActionResult ChangeRole(RoleList _roleList)
		{
			List<string> roleStr = new List<string>();
			foreach(var item in _roleList.roles){
				if(item.IsChecked){
					var tester = item.RoleName;
					roleStr.Add(tester);
				}
			}
            
			ChangeUserRole changeuserrole = new ChangeUserRole("titatest@test.nl", roleStr[0]);
            
			var stringJson = JsonConvert.SerializeObject(changeuserrole);
			var jsonObject = JObject.Parse(stringJson);

			var returnType = _users.ChangeRoleOfUser(jsonObject);

			return RedirectToAction("Index");
		}
        
		[HttpPost]
		public IActionResult ChangeReservationLimit(UserReservationLimit u)
        {
			ChangeReservationLimit changeReservationLimit = new ChangeReservationLimit("titatest@test.nl", u._reservationLimit);

			var stringJson = JsonConvert.SerializeObject(changeReservationLimit);
            var jsonObject = JObject.Parse(stringJson);
            
			var returnType = _users.ChangeReservationLimitOfUser(jsonObject);

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
		public IActionResult AddRole(string role)
		{
			AddNewRole newRole = new AddNewRole(role);
            
			var stringJson = JsonConvert.SerializeObject(newRole);
            var jsonObject = JObject.Parse(stringJson);

			var returnType = _users.AddRole(jsonObject);

			return RedirectToAction("Index");
		}
    }
}