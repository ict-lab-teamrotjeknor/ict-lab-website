using System;
namespace ict_lab_website.Models.Users
{
    public class DeleteUser
    {
		public string Email { get; set; }

		public DeleteUser(string _email)
        {
			Email = _email;
        }
    }
}
