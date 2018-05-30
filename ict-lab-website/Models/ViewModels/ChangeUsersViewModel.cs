using System;
namespace ict_lab_website.Models.ViewModels
{
    public class ChangeUsersViewModel
    {
		public enum Role
        {
            Guest = 1,
            Student = 2,
            Teacher = 3,
            Handyman = 4,
            Servicedesk = 5,
            Rastermaker = 6,
            Administrator = 7
        }

        public Role _role { get; set; }
    }
}
