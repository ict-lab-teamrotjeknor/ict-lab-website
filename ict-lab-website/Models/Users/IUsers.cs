using System;
using Newtonsoft.Json.Linq;

namespace ict_lab_website.Models.Users
{
    public interface IUsers
    {
		string GetAllUsers();
		JObject ChangeRoleOfUser(JObject jsonObject);
		JObject ChangeReservationLimitOfUser(JObject reservationlimitObject);
		JObject AddRole(JObject addroleObject);
		JObject DeleteUser(JObject deleteuserObject);
		JObject CheckRole(JObject checkroleObject);
		JObject CheckReservationLimit(JObject checkreservationlimitObject);
    }
}
