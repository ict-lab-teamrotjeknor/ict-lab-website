using System;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Users
{
    public interface IUsers
    {
		string GetAllUsers();
		JObject ChangeRoleOfUser(JObject jsonObject);
		JObject ChangeReservationLimitOfUser(JObject jsonObject);
		JObject AddRole(JObject jsonObject);
		JObject DeleteAnUser(JObject jsonObject);
		//JObject CheckRole(JObject jsonObject);
		//JObject CheckReservationLimit(JObject jsonObject);
    }
}
