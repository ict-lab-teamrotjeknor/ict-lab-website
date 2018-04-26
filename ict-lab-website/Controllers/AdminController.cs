using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ictlabwebsite.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult usersOverview()
        {
            return View();
        }
    }
}
