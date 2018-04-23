using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Rooms
{
    public class Reservation
    {
        public string RoomName { get; set; }
        public DateTime Date { get; set; }
        public int StartLessonHour { get; set; }
        public int EndLessonHour { get; set; }
        public string Reserver { get; set; }
        public string Subject { get; set; }
    }
}
