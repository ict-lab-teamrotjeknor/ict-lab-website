﻿using System;
using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Home
{
	public class HomeCredentials : IHomeCredentials
    {
		private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;
		private readonly ILogger _logger;

		public HomeCredentials(IOptions<ApiConfig> apiConfig, ILogger<HomeController> logger)
        {
			apiCalls = new ApiCalls();
			this.apiConfig = apiConfig.Value;
			_logger = logger;
        }
        
		public JObject LoginCredentials(JObject loginObject)
		{
			return apiCalls.PostRequest(loginObject, apiConfig.Url + apiConfig.SignIn);
		}
        
		public JObject RegisterCredentials(JObject registerObject)
		{
			return apiCalls.PostRequest(registerObject, apiConfig.Url + apiConfig.SignUp);
		}
	}
}