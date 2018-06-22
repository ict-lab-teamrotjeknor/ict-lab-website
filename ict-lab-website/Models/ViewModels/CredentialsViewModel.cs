using System;
using System.ComponentModel.DataAnnotations;

namespace ict_lab_website.Models.ViewModels
{
    public class CredentialsViewModel
    {
		[Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
