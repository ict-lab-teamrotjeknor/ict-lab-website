using System;
using System.Collections.Generic;
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
	public class Users : IUsers
    {
		//private List<User> users;
		private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;
        private readonly ILogger _logger;
        
		public Users(IOptions<ApiConfig> apiConfig, ILogger<UsersController> logger)
        {
			apiCalls = new ApiCalls();
			this.apiConfig = apiConfig.Value;
			_logger = logger;
			//users = GetUsers();
        }

		public JObject AddRole(JObject jsonObject)
		{
			return apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.AddRole);
		}

		public JObject ChangeReservationLimitOfUser(JObject jsonObject)
		{
			return apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeReservationLimit);
		}

		public JObject ChangeRoleOfUser(JObject jsonObject)
		{
			return apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeRole);
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
			return apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.DeleteUser);
		}
        
		public string GetAllUsers()
		{
			return apiCalls.GetRequest(apiConfig.Url + apiConfig.GetUsers);
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
