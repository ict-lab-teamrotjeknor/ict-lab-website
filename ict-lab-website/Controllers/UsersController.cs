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

namespace ictlabwebsite.Controllers
{
    public class UsersController : Controller
    {
        private ApiCalls _apiCalls;

        public UsersController()
        {
            _apiCalls = new ApiCalls();
        }
        
        [HttpGet]
        public IActionResult Index()
        {
			List<ChangeRole> obj = new List<ChangeRole>();
			obj.Add(new ChangeRole() { Text = "Guest", Value = 1, IsChecked = false });
			obj.Add(new ChangeRole() { Text = "Student", Value = 2, IsChecked = false });
			obj.Add(new ChangeRole() { Text = "Teacher", Value = 3, IsChecked = false });
			obj.Add(new ChangeRole() { Text = "Handyman", Value = 4, IsChecked = false });
			obj.Add(new ChangeRole() { Text = "Servicedesk", Value = 5, IsChecked = false });
			obj.Add(new ChangeRole() { Text = "Rastermaker", Value = 6, IsChecked = true });
			obj.Add(new ChangeRole() { Text = "Administrator", Value = 7, IsChecked = false });

            var returnType = _apiCalls.GetRequest("http://145.24.222.103:8080/manage/getusers");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
            DataTable dataTable = dataSet.Tables["Users"];

			RoleList objBind = new RoleList();
			objBind.Roles = obj;

			UsersViewModel viewModel = new UsersViewModel(dataTable, objBind);
            
            return View(viewModel);
        }
        
		[HttpPost]
		public IActionResult Index(RoleList r)
		{
			var test1 = r;
			var test2 = "test";
			var test3 = "test";

			StringBuilder stringBuilder = new StringBuilder();

			foreach(var item in r.Roles){
				if(item.IsChecked){
					stringBuilder.Append(item.Text + ", ");
				}
			}
			ViewBag.selectRole = stringBuilder.ToString();

			return View(r);
		}
        
		//[HttpPost]
		//public IActionResult changeReservationLimit(ChangeReservationLimit c)
        //{
			//var test = c;

			//var stringJson = JsonConvert.SerializeObject(viewModel);
        //    var rJson = JObject.Parse(stringJson);

        //    var returnType = _apiCalls.PostRequest(rJson, "http://145.24.222.103:8080/authentication/signup");

        //    return RedirectToAction("Index");
        //}
    }
}