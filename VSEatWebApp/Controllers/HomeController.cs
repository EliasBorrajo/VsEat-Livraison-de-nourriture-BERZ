using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VSEatWebApp.Models;

namespace VSEatWebApp.Controllers
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
            IActionResult rv = View();
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                rv = RedirectToAction("Index", "Staff");
            }
            else if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                rv = RedirectToAction("Index", "Client");
            }
            return rv;
        }

        public IActionResult MyAccount()
        {
            IActionResult rv = RedirectToAction("Login", "Client");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                rv = RedirectToAction("Edit", "Staff");
            }
            else if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                rv = RedirectToAction("Edit", "Client");
            }
            return rv;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
