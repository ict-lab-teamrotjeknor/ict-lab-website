using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Models.Schedule.Views;
using ict_lab_website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ict_lab_website.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ISchedule Schedule;
        private readonly ILogger logger;

        public ScheduleController(ISchedule schedule, ILogger<ScheduleController> logger)
        {
            this.Schedule = schedule;
            this.logger = logger;
        }

        public IActionResult Index(string roomName, DateTime dateTime, string view)
        {
            RoomScheduleViewModel roomReservationsViewModel = new RoomScheduleViewModel(roomName, view, dateTime, Schedule);
            return View(roomReservationsViewModel);
        }

        [HttpGet]
        public IActionResult AddReservation(string roomName, int startLessonHour)
        {
            ViewBag.roomName = roomName;
            ViewBag.startLessonHour = startLessonHour;
            return View("AddReservation");
        }

        [HttpPost]
        public IActionResult AddReservation(UploadableReservation reservation)
        {
            if (ModelState.IsValid)
            {
                Schedule.AddReservation(reservation);
                return RedirectToAction("Index", "Rooms", new { area = "" });
            }
            return View(reservation);
        }
    }
}