﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppEfCoreOidc.Models;

namespace WebAppEfCoreOidc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Private()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult OpenDoor()
        {
            if (User.IsInRole(Roles.Custodian))
            {
                return View();
            }

            return Forbid();
        }

        [Authorize(Roles = Roles.Custodian)]
        public IActionResult CloseDoor()
        {
            return View();
        }

        [Authorize(Policy = Policies.GradesView)]
        public IActionResult ViewGrades()
        {
            return View();
        }

        [Authorize(Policy = Policies.GradesEdit)]
        public IActionResult EditGrades()
        {
            return View();
        }
    }
}
