using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ict_lab_website.Models.Notifications;
using ict_lab_website.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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

        public IActionResult Index(string IsNotificationAdded)
        {
            var checkInternetConnection = CheckInternetConnection.CheckConnection();

            if (checkInternetConnection == false)
            {
                ViewBag.role = HttpContext.Session.GetString("Role");
                ViewBag.internet = checkInternetConnection;
                return View();
            }

            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                ViewBag.IsNotificationAdded = IsNotificationAdded;
                ViewBag.role = HttpContext.Session.GetString("Role");
                var notifications = repository.GetAll();
                return View(notifications);
            } else{
                return RedirectToAction("NotAuthorized", "Home");
            }
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
                    return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Notifications", action = "Index", IsNotificationAdded = "success" }));
                }
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Notifications", action = "Index", IsNotificationAdded = "failed" }));
        }
    }
}