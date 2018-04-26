using System;
using System.ComponentModel.DataAnnotations;

namespace ict_lab_website.Models.Users
{
    public class Users
    {
        public Guid userId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public enum roles 
        {
            Guest = 1,
            Student = 2,
            Teacher = 3,
            Handyman = 4,
            Servicedesk = 5,
            Rastermaker = 6,
            Administrator = 7
        }
        public int reservationLimit { get; set; }
    }
}
