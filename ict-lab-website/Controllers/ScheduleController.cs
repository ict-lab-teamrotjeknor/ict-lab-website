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
        public IActionResult AddReservation(string roomName, int startLessonHour, DateTime dateTime)
        {
            ViewBag.Year = dateTime.Year;
            ViewBag.Week = Schedule.GetWeekNumber(dateTime);
            ViewBag.RoomName = roomName;
            ViewBag.StartLessonHour = startLessonHour;
            ViewBag.IsReservationAdded = true; 
            var culture = new System.Globalization.CultureInfo("nl-NL");
            ViewBag.Day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);

            return View();
        }

        [HttpPost]
        public IActionResult AddReservation(UploadableReservation reservation)
        {
            
            if (ModelState.IsValid)
            {
                Boolean isReservationAdded = Schedule.AddReservation(reservation);

                if (isReservationAdded)
                {
                    return RedirectToAction("Index", "Rooms", new { area = "" });
                }
            }

            ViewBag.Week = reservation.Week;
            ViewBag.Day = reservation.Day;
            ViewBag.Year = reservation.Year;
            ViewBag.RoomName = reservation.Classroom;
            ViewBag.StartLessonHour = reservation.StartHour;
            ViewBag.IsReservationAdded = false;
            return View(reservation);
        }
    }
}