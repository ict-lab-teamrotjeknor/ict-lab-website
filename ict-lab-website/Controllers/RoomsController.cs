using System;
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

        public IActionResult Index()
        {
            return View(repository.Rooms);
        }

        public IActionResult Details(int ID)
        {
            Room room = repository.GetById(ID);
            return View(room);
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
            return RedirectToAction("Details", new { id = room.ID });
        }

    }
}
