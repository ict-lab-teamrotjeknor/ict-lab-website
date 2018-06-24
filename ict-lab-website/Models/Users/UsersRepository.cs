using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using ict_lab_website.Process;
using ictlabwebsite.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Users
{
	public class UsersRepository : IUsers
    {
		private readonly IApiCalls _apiCalls;
        private readonly ApiConfig apiConfig;
        private readonly ILogger _logger;
		private JObject apiCall;
		private string apiCallString;
        
		public UsersRepository(IOptions<ApiConfig> apiConfig, ILogger<UsersController> logger, IApiCalls apiCalls)
        {
			_apiCalls = apiCalls;
			this.apiConfig = apiConfig.Value;
			_logger = logger;
			apiCall = new JObject();
        }

		public JObject AddRole(JObject jsonObject, string userToken)
		{         
            try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.AddRole, userToken);
            }
            catch (Exception e)
            {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - {file} : {line}] Cannot make postrequest to API", e);
            }
            return apiCall;
		}

		public JObject ChangeRoleOfUser(JObject jsonObject, string userToken)
		{
            try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeRole, userToken);
            }
            catch (Exception e)
            {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - {file} : {line}] Cannot make postrequest to API", e);
            }
            return apiCall;
		}

        public string CheckRole(string Email)
        {
            try
            {
                apiCallString = _apiCalls.GetRequest(apiConfig.Url + apiConfig.CheckRole + "/" + Email);
            }
            catch (Exception e)
            {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - {file} : {line}] Cannot get users from API");
            }
            return apiCallString;
        }

		public JObject DeleteAnUser(JObject jsonObject, string userToken)
		{
			try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.DeleteUser, userToken);
            }
            catch (Exception e)
            {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - {file} : {line}] Cannot make postrequest to API", e);
            }
            return apiCall;
		}
        
		public string GetAllUsers()
		{
			try
            {
				apiCallString = _apiCalls.GetRequest(apiConfig.Url + apiConfig.GetUsers);
            }
            catch (Exception e)
            {
                var stackTrace = new StackTrace(e, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                var file = frame.GetFileName();

                _logger.LogError($"{DateTime.Now} - {file} : {line}] Cannot get users from API");
            }
			return apiCallString;
		}
	}
}
