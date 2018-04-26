using System;
using System.ComponentModel.DataAnnotations;

namespace ict_lab_website.Models.Admin
{
    public class Users
    {
        public Guid userId;
        public string name;
        public string surname;
        public string email;
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
        public int reservationLimit;
    }
}
