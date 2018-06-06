using System;
namespace ict_lab_website.Models.Users
{
    public class AddNewRole
    {
		public string role { get; set; } 

        public AddNewRole(string _role)
        {
			role = _role;
        }
    }
}
