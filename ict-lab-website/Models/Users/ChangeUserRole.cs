using System;
namespace ict_lab_website.Models.Users
{
    public class ChangeUserRole
    {
		public string UserEmail { get; set; }
        public string RoleName { get; set; }
        
		public ChangeUserRole(string _Email, string _Role)
		{
			UserEmail = _Email;
			RoleName = _Role;
		}
    }
}
