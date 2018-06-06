using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Process;
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

        public RoomsController(IRepository<Room> roomRepository, ILogger<RoomsController> logger)
        {
            this.repository = roomRepository;
            this.logger = logger;
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

            ViewBag.date = date;
            return View(rooms);
        }
    }
}
