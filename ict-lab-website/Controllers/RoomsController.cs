using System;
using System.Linq;
using ict_lab_website.Models;
using Microsoft.AspNetCore.Mvc;

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        private IRoomRepository repository;

        public RoomsController(IRoomRepository repository)
        {
            this.repository = repository;
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

        public IActionResult AddReservation()
        {
            return View();
        }
    }
}
