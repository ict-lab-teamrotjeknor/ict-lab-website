using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Process
{
    public class ApiConfig
    {
        public string Url { get; set; }
        public string GetAllRooms { get; set; }
        public string GetWeek { get; set; }
        public string GetUsers { get; set; }
        public string SignIn { get; set; }
        public string SignUp { get; set; }
        public string UploadHour { get; set; }
		public string ChangeRole { get; set; }
		public string AddRole { get; set; }
		public string DeleteUser { get; set; }
		public string CheckRole { get; set; }
        public string GetAllNotifications { get; set; }
        public string SendNotification { get; set; }
        public string SendNotificationToGroup { get; set; }
        public string GetRoles { get; set; }
    }
}
