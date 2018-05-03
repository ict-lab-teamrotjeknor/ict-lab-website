using ict_lab_website.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ict_lab_website.Models;
using ict_lab_website.Models.Schedule;

namespace ict_lab_website.Models.ViewModels
{
    public class RoomScheduleViewModel
    {
        public Room Room { get;}
        public ScheduleView View { get; }
        public DateTime DateAndTime { get; }

        public RoomScheduleViewModel(Room room, ScheduleView view, DateTime dateTime)
        {
            this.Room = room;
            this.View = view;
            this.DateAndTime = dateTime;
        }
    }
}
