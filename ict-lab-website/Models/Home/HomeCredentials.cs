using System;
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
		private JObject apiCall;

		public HomeCredentials(IOptions<ApiConfig> apiConfig, ILogger<HomeController> logger)
        {
			apiCalls = new ApiCalls();
			this.apiConfig = apiConfig.Value;
			_logger = logger;
			apiCall = new JObject();
        }
        
		public JObject LoginCredentials(JObject jsonObject)
		{
			try {
				apiCall = apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.SignIn);
			} catch(Exception e) {
				_logger.LogError("Cannot make postrequest to API", e);
			}
			return apiCall;
		}
        
		public JObject RegisterCredentials(JObject jsonObject)
		{
			try {
				apiCall = apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.SignUp);
			} catch(Exception e){
				_logger.LogError("Cannot make postrequest to API", e);
			}
			return apiCall;
		}
	}
}
