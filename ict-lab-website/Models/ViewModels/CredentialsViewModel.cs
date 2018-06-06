using System;
using System.ComponentModel.DataAnnotations;

namespace ict_lab_website.Models.ViewModels
{
    public class CredentialsViewModel
    {
		[Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
