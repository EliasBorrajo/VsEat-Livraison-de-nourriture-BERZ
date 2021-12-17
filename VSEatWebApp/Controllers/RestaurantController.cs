using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSEatWebApp.Models;

namespace VSEatWebApp.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }

        public RestaurantController(IRestaurantManager RestaurantManager)
        {
            this.RestaurantManager = RestaurantManager;
        }

        public IActionResult Index()
        {
            IActionResult rv = RedirectToAction("Index", "Client");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                rv = View(RestaurantManager.GetRestaurants());
            }
            return rv;
        }
    }
}
