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
            var returnType = _apiCalls.GetRequest("http://145.24.222.103:8080/manage/getusers");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
            DataTable dataTable = dataSet.Tables["Users"];

			UsersViewModel viewModel = new UsersViewModel(dataTable);
            return View(viewModel);
        }

		[HttpPost]
		public IActionResult changeRole(ChangeRole c)
		{
			var test = c;
			var test2 = "hoi";
			var test3 = "hoi";
			var test4 = "hoi";
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