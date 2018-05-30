using System;
using System.Collections.Generic;
using System.Linq;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace ict_lab_website.Controllers
{
    public class RoomsController : Controller
    {
        private IRepository<Room> Repository;

        public RoomsController(IRepository<Room> roomRepository)
        {
            this.Repository = roomRepository;
        }

        public IActionResult Index(DateTime date, string searchString = "H.")
        {
            var rooms = Repository.GetAll();

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
