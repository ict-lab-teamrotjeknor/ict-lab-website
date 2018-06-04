﻿using System;
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

namespace ictlabwebsite.Controllers
{
    public class UsersController : Controller
    {
        private ApiCalls _apiCalls;
		private readonly ILogger _logger;

		public UsersController(ILogger<UsersController> logger)
        {
            _apiCalls = new ApiCalls();
			_logger = logger;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
			List<Roles> ro = new List<Roles>();
			ro.Add(new Roles() { RoleId = 1, RoleName = "Guest", IsChecked = false });
			ro.Add(new Roles() { RoleId = 2, RoleName = "Student", IsChecked = false });
			ro.Add(new Roles() { RoleId = 3, RoleName = "Teacher", IsChecked = false });
			ro.Add(new Roles() { RoleId = 4, RoleName = "Handyman", IsChecked = true });
			ro.Add(new Roles() { RoleId = 5, RoleName = "Servicedesk", IsChecked = false });
			ro.Add(new Roles() { RoleId = 6, RoleName = "Rastermaker", IsChecked = false });
			ro.Add(new Roles() { RoleId = 7, RoleName = "Administrator", IsChecked = false });
            
			RoleList roleList = new RoleList();
			roleList.roles = ro;

            var returnType = _apiCalls.GetRequest("http://145.24.222.103:8080/manage/getusers");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);
            DataTable dataTable = dataSet.Tables["Users"];

			UsersViewModel viewModel = new UsersViewModel(dataTable, roleList);
            
            return View(viewModel);
        }
        
		[HttpPost]
		public IActionResult Index(RoleList _roleList)
		{

			StringBuilder stringBuilder = new StringBuilder();

			foreach(var item in _roleList.roles){
				if(item.IsChecked){
					stringBuilder.Append(item.RoleName + ", ");
				}
			}
			ViewBag.selectRole = stringBuilder.ToString();

			return View(_roleList);
		}
        
		//[HttpPost]
		//public IActionResult changeReservationLimit(ChangeReservationLimit c)
   //     {
			//var test = c;

			//var stringJson = JsonConvert.SerializeObject(viewModel);
        //    var rJson = JObject.Parse(stringJson);

        //    var returnType = _apiCalls.PostRequest(rJson, "http://145.24.222.103:8080/authentication/signup");

        //    return RedirectToAction("Index");
        //}
        
		public IActionResult deleteUser(string Email)
		{
			return RedirectToAction("Index");
		}
    }
}