using System;
using System.Collections.Generic;
using System.Linq;
using ict_lab_website.Models;
using Microsoft.AspNetCore.Mvc;

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        private IRoomRepository repository;

        public RoomsController()
        {
            this.repository = FakeRoomRepository.SharedRepository;
        }

        public IActionResult Index(string searchString)
        {
            var rooms = repository.Rooms;
            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(room => room.RoomCode.Contains(searchString));
            }

            return View(rooms);            
        }

        public IActionResult Schedule(int ID, DateTime dateTime, ScheduleView view = ScheduleView.Day)
        {
            Room room = repository.GetById(ID);
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
            return View("Index", repository.Rooms);
        }

        [HttpGet]
        public IActionResult AddReservation()
        {
            return View(new Reservation());
        }

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            Room room = repository.GetById(reservation.RoomID);
            room.Reservations.Add(reservation);
            return RedirectToAction("Schedule", new { id = room.ID, dateTime = reservation.DateAndTime});
        }

    }
}
