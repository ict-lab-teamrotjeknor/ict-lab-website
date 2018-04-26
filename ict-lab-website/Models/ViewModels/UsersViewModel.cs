using System;
using System.Collections.Generic;
using ict_lab_website.Models;
namespace ict_lab_website.Models.ViewModels
{
    public class UsersViewModel
    {
        public List<string> _userId { get; set; }
        public List<string> _email { get; set; }

        public UsersViewModel(List<string> userId, List<string> email)
        {
            _userId = userId;
            _email = email;
        }
    }
}
