using System;
using System.Linq;
using ict_lab_website.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
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
