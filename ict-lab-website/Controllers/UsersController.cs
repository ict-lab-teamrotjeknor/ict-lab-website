﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ict_lab_website.Process;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Models.Users;
using Newtonsoft.Json;
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

        public IActionResult Index(DataTable _dataTable)
        {
            foreach(DataRow row in _dataTable.Rows)
            {
                var test = row["Id"];
            }

            UsersViewModel viewModel = new UsersViewModel(_dataTable);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var returnType = _apiCalls.GetRequest("http://145.24.222.103:8080/manage/getusers");

            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(returnType);

            DataTable dataTable = dataSet.Tables["Users"];

            return RedirectToAction("Index", new { _dataTable = dataTable });
        }
    }
}