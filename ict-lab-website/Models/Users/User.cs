using System;
namespace ict_lab_website.Models.Users
{
    public class User
    {
		public string email { get; set; }

        public User(string _email)
		{
			email = _email;
		}
    }
}
