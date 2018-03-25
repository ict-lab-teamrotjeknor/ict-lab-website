using System;
using System.Linq;
using ict_lab_website.Models;
using Microsoft.AspNetCore.Mvc;

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            Repository.GenerateExampleData();
            return View(Repository.Rooms);
        }

        public IActionResult Details(int ID)
        {
            Room room = Repository.GetRoom(ID);
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
            Repository.AddRoom(room);
            return View("Index", Repository.Rooms);
        }

        public IActionResult AddReservation()
        {
            return View();
        }
    }
}
