using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRepository<Room> repository;
        private readonly ILogger logger;
        private readonly ISchedule schedule;

        public RoomsController(IRepository<Room> roomRepository, ILogger<RoomsController> logger, ISchedule schedule)
        {
            this.repository = roomRepository;
            this.logger = logger;
            this.schedule = schedule;
        }

        public IActionResult Index(DateTime date, string searchString = "H.")
        {
            List<Room> rooms = repository.GetAll();            

            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(room => room.Name.Contains(searchString)).ToList();
            }

            if (date.Equals(new DateTime()))
            {
                date = DateTime.Now;
            }

            Dictionary<Room, int> roomsAndTimeSlots = new Dictionary<Room, int>(); 
            foreach(Room room in rooms)
            {
                int numberOfFreeTimeslots = schedule.GetNumberOfFreeTimeslots(date, room.Name);
                roomsAndTimeSlots.Add(room, numberOfFreeTimeslots);
            }
            ViewBag.role = HttpContext.Session.GetString("Role");
            ViewBag.date = date;
            return View(roomsAndTimeSlots);
        }
    }
}
