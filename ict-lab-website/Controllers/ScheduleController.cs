using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ict_lab_website.Controllers
{
    public class ScheduleController : Controller
    {
        private ISchedule Schedule;

        public ScheduleController(ISchedule schedule)
        {
            this.Schedule = schedule;
        }

        public IActionResult Index(string roomName, DateTime dateTime, string view = "ScheduleViewDay")
        {
            RoomScheduleViewModel roomReservationsViewModel = new RoomScheduleViewModel(roomName, view, dateTime, this.Schedule);
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
        public IActionResult AddReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}