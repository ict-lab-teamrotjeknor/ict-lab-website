using System;
using ict_lab_website.Process;
using ictlabwebsite.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Users
{
	public class Users : IUsers
    {
		private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;
        private readonly ILogger _logger;

		public Users(IOptions<ApiConfig> apiConfig, ILogger<UsersController> logger)
        {
			apiCalls = new ApiCalls();
			this.apiConfig = apiConfig.Value;
			_logger = logger;
        }

		public JObject AddRole(JObject addroleObject)
		{
			throw new NotImplementedException();
		}

		public JObject ChangeReservationLimitOfUser(JObject jsonObject)
		{
			return apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeReservationLimit);
		}

		public JObject ChangeRoleOfUser(JObject jsonObject)
		{
			return apiCalls.PostRequest(jsonObject, apiConfig.Url + apiConfig.ChangeRole);
		}

		public JObject CheckReservationLimit(JObject checkreservationlimitObject)
		{
			throw new NotImplementedException();
		}

		public JObject CheckRole(JObject checkroleObject)
		{
			throw new NotImplementedException();
		}

		public JObject DeleteUser(JObject deleteuserObject)
		{
			throw new NotImplementedException();
		}

		public string GetAllUsers()
		{
			return apiCalls.GetRequest(apiConfig.Url + apiConfig.GetUsers);
		}
	}
}
