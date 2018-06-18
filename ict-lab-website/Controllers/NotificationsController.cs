using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ict_lab_website.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ict_lab_website.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationRepository repository;
        private readonly ILogger logger;

        public NotificationsController(INotificationRepository repository, ILogger<NotificationsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var notifications = repository.GetAll();
            return View(notifications);
        }

        [HttpPost]
        public IActionResult SendNotification(UploadableNotification notification)
        {
            if (ModelState.IsValid)
            {
                Boolean isNotificationSend = repository.SendNotification(notification);

                if (isNotificationSend)
                {
					ViewBag.IsNotificationAdded = true;
                    return RedirectToAction("Index", "Notifications", new { area = "" });
                }
            }

            ViewBag.IsNotificationAdded = false;
            return RedirectToAction("Index", "Notifications", new { area = "" });
        }

        [HttpPost]
        public IActionResult SendNotificationToGroup(UploadableGroupNotification notification)
        {
            if (ModelState.IsValid)
            {
                Boolean isNotificationSend = repository.SendNotificationToGroup(notification);

                if (isNotificationSend)
                {
					ViewBag.IsNotificationAdded = true;
                    return RedirectToAction("Index", "Notifications", new { area = "" });
                }
            }
            var test = "test";
            ViewBag.IsNotificationAdded = false;
            return RedirectToAction("Index", "Notifications", new { area = "" });
        }
    }
}