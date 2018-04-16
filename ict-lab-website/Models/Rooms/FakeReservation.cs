using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Rooms
{
    //NOTE: This class will be deprecated once the schedule-functionality of the API becomes available.
    public class FakeReservation
    {
        public string RoomName { get; set; }
        public DateTime DateAndTime { get; set; }
        public int Length { get; set; }
        public string Reserver { get; set; }
        public string Subject { get; set; }
    }
}
