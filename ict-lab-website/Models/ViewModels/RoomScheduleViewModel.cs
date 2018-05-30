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
        public string RoomName { get; }
        public string View { get; }
        public DateTime DateAndTime { get; }
        public ISchedule Schedule { get; }

        public RoomScheduleViewModel(string roomName, string view, DateTime dateTime, ISchedule schedule)
        {
            this.RoomName = roomName;
            this.View = view;
            this.DateAndTime = dateTime;
            this.Schedule = schedule;
        }
    }
}
