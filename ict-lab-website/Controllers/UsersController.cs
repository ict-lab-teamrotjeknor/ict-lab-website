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

namespace ictlabwebsite.Controllers
{
    public class UsersController : Controller
    {
        private ApiCalls _apiCalls;

        public UsersController()
        {
            _apiCalls = new ApiCalls();
        }

        public IActionResult Index()
        {
			List<ChangeRole> obj = new List<ChangeRole>(){
				new ChangeRole { Text="Guest", Value=1, IsChecked=true },
				new ChangeRole { Text="Student", Value=2, IsChecked=false },
				new ChangeRole { Text="Teacher", Value=2, IsChecked=false },
				new ChangeRole { Text="Handyman", Value=2, IsChecked=false },
				new ChangeRole { Text="Servicedesk", Value=2, IsChecked=false },
				new ChangeRole { Text="Rastermaker", Value=2, IsChecked=false },
				new ChangeRole { Text="Administrator", Value=2, IsChecked=false }
			};

            var returnType = _apiCalls.GetRequest("http://145.24.222.103:8080/manage/getusers");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
            DataTable dataTable = dataSet.Tables["Users"];

			RoleList objBind = new RoleList();
			objBind.Roles = obj;

			UsersViewModel viewModel = new UsersViewModel(dataTable, objBind);

            return View(viewModel);
        }
        
		[HttpPost]
		public IActionResult Index(RoleList obj)
		{
			return RedirectToAction("Index");
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