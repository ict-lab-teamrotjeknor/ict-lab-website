using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ict_lab_website.Models.Schedule;

namespace ict_lab_website.Models.Rooms
{
    // NOTE: This class will be completely changed once the schedule-functionality of the API becomes available.
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RoomSchedule RoomSchedule { get; set; }

        public Room()
        {
            RoomSchedule = new RoomSchedule();
        }
    }
}
