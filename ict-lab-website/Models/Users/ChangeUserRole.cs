using System;
namespace ict_lab_website.Models.Users
{
    public class ChangeUserRole
    {
		public string email { get; set; }
        public string role { get; set; }
        
		public ChangeUserRole(string _Email, string _Role)
		{
			email = _Email;
			role = _Role;
		}
    }
}
