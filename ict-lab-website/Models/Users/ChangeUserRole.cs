using System;
namespace ict_lab_website.Models.Users
{
    public class ChangeUserRole
    {
		public string Email { get; set; }
        public string Role { get; set; }

		public ChangeUserRole(string _Email, string _Role)
		{
			Email = _Email;
			Role = _Role;
		}
    }
}
