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
                List<RestaurantVM> restaurantVMs = new List<RestaurantVM>();
                DTO.Restaurant[] restaurants = RestaurantManager.GetRestaurants();
                foreach (DTO.Restaurant restaurant in restaurants)
                {
                    restaurantVMs.Add(new RestaurantVM() { ID = restaurant.ID, Nom = restaurant.Nom, NomLocalite = restaurant.Localite.Nom, Plats = restaurant.Plats });
                }
                rv = View(restaurantVMs);
            }
            return rv;
        }
    }
}
