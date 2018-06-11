using System;
using System.Collections.Generic;

namespace ict_lab_website.Models.Users
{
    public class Roles
    {
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public bool IsChecked { get; set; }
    }

    public class RoleList
	{
		public List<Roles> roles { get; set; }
		public string Email { get; set; }
	}
}
