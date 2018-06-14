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
    }
}