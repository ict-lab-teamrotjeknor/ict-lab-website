using System;
using System.Collections.Generic;
using System.Linq;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        private IRepository<Room> repository;

        public RoomsController(IRepository<Room> roomRepository)
        {
            this.repository = roomRepository;
        }

        public IActionResult Index(DateTime reserveableOn, string searchString = "H.")
        {
            var rooms = repository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(room => room.Name.Contains(searchString));
            }

            return View(rooms);            
        }

        public IActionResult Schedule(string Id, DateTime dateTime, ScheduleView view = ScheduleView.Day)
        {
            Room room = repository.GetById(Id);
            RoomReservationsViewModel roomReservationsViewModel = new RoomReservationsViewModel(room, view, dateTime);
            return View(roomReservationsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            repository.Add(room);
            return View("Index", repository.GetAll());
        }

        [HttpGet]
        public IActionResult AddReservation()
        {
            return View(new Reservation());
        }

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            Room room = repository.GetByName(reservation.RoomName);
            room.Reservations.Add(reservation);
            return RedirectToAction("Schedule", new { id = room.Id, dateTime = reservation.Date});
        }

    }
}
