﻿using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.ViewModels
{
    public class RoomReservationsViewModel
    {
        public Room Room { get;}
        public List<FakeReservation> Reservations { get;}
        public ScheduleView View { get; }
        public DateTime DateAndTime { get; }

        public RoomReservationsViewModel(Room room, ScheduleView view, DateTime dateTime)
        {
            this.Room = room;
            this.View = view;
            this.Reservations = room.GetReservationsFor(view, dateTime);
            this.DateAndTime = dateTime;
        }
    }
}