﻿using System;
using System.Collections.Generic;
using System.Linq;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.Schedule;
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

        public IActionResult Index(DateTime date, string searchString = "H.")
        {
            var rooms = repository.GetAll();

            var b = true;

            if (!rooms.Any()){
                b = false;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(room => room.Name.Contains(searchString));
            }

            if (date.Equals(new DateTime()))
            {
                date = DateTime.Now;
            }

            ViewBag.date = date;
            ViewBag.b = b;
            return View(rooms);            
        }

        public IActionResult Schedule(string name, DateTime dateTime, ScheduleView view = ScheduleView.Day)
        {
            Room room = repository.GetByName(name);
            RoomScheduleViewModel roomReservationsViewModel = new RoomScheduleViewModel(room, view, dateTime);
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

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                Room room = repository.GetByName(reservation.RoomName);
                room.RoomSchedule.AddReservation(reservation);
            }
            return View("Index");
        }
    }
}
