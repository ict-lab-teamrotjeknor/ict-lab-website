﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Notifications
{
    public class Notification
    {
        public string ID { get; set; }
        public string Role { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
    }
}
