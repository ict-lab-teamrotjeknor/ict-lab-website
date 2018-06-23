using System;
using System.Diagnostics;
using ict_lab_website.Controllers;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Home
{
	public class HomeCredentials : IHomeCredentials
    {
		private readonly IApiCalls _apiCalls;
        private readonly ApiConfig apiConfig;
		private readonly ILogger _logger;
		private JObject apiCall;

		public HomeCredentials(IOptions<ApiConfig> apiConfig, ILogger<HomeController> logger, IApiCalls apiCalls)
        {
            _apiCalls = apiCalls;
			this.apiConfig = apiConfig.Value;
			_logger = logger;
			apiCall = new JObject();
        }
        
		public JObject LoginCredentials(JObject jsonObject)
		{
			try {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.SignIn);
			} catch(Exception e) {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - [{file} : {line}] Cannot make post-request to API");
			}
			return apiCall;
		}
        
		public JObject RegisterCredentials(JObject jsonObject)
		{
			try {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.SignUp);
			} catch(Exception e){
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - [{file} : {line}] Cannot make post-request to API");
            }
			return apiCall;
		}
	}
}
