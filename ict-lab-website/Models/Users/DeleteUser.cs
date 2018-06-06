using System;
namespace ict_lab_website.Models.Users
{
    public class DeleteUser
    {
		public string email { get; set; }

		public DeleteUser(string _email)
        {
            email = _email;
        }
    }
}
