using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models
{
    public class Reservation
    {
        public string RoomName { get; set; }
        public DateTime DateAndTime { get; set; }
        public int Length { get; set; }
        public string Reserver { get; set; }
        public string Subject { get; set; }
    }
}
