using System;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Users
{
    public interface IUsers
    {
		string GetAllUsers();
		JObject ChangeRoleOfUser(JObject jsonObject, string userToken);
		JObject AddRole(JObject jsonObject, string userToken);
		JObject DeleteAnUser(JObject jsonObject, string userToken);
		string CheckRole(string Email, string token);
    }
}
