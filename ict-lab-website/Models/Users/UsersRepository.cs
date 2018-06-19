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
		//private List<User> users;
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
			//users = GetUsers();
        }

		public JObject AddRole(JObject jsonObject)
		{         
            try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.AddRole);
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

		public JObject ChangeReservationLimitOfUser(JObject jsonObject)
		{
			try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeReservationLimit);
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

		public JObject ChangeRoleOfUser(JObject jsonObject)
		{
            try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeRole);
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

		//public JObject CheckReservationLimit(JObject jsonObject)
		//{
			//throw new NotImplementedException();
		//}
        
		//public JObject CheckRole(JObject jsonObject)
		//{
			//throw new NotImplementedException();
		//}

		public JObject DeleteAnUser(JObject jsonObject)
		{
			try
            {
				apiCall = _apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.DeleteUser);
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
        
        //      public List<User> GetUsers()
    	//{
    	//	List<User> users = new List<User>();

    	//	try
        //          {
    	//		var json = apiCalls.GetRequest(apiConfig.Url + apiConfig.GetUsers);
        //              var classRooms = JObject.Parse(json)["Name"];

        //              foreach (JToken classroom in classRooms)
        //              {
        //                  foreach (JToken room in classroom.Children())
        //                  {
        //                      User u = JsonConvert.DeserializeObject<User>(room.ToString());
        //                      users.Add(u);
        //                  }
        //              }
        //          }
        //          catch (WebException e)
        //          {
        //              _logger.LogError("Cannot get users from API", e);
        //          }
                
        //	return users;
    	//}

        //public User GetUserByName(string email)
		//{
		//	return users.Where(users => users.email.Equals(email)).First() ?? new User();
		//}
	}
}
