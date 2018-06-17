using System;
namespace ict_lab_website.Models.Users
{
    public class DeleteUser
    {
		public string UserEmail { get; set; }

		public DeleteUser(string _email)
        {
			UserEmail = _email;
        }
    }
}
