using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VSEatWebApp.Controllers
{
    /// <summary>
    /// Classe controller pour tout ce qui concerne les restaurants.
    /// </summary>
    public class RestaurantController : Controller
    {
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes des restaurants.
        /// </summary>
        private IRestaurantManager RestaurantManager { get; }
        /// <summary>
        /// Constructeur du RestaurantController.
        /// </summary>
        /// <param name="RestaurantManager">Instance du RestaurantManager.</param>
        public RestaurantController(IRestaurantManager RestaurantManager)
        {
            this.RestaurantManager = RestaurantManager;
        }
        /// <summary>
        /// Action affichant la liste des restaurants.
        /// </summary>
        /// <returns>Vue liste des restaurants.</returns>
        public IActionResult Index()
        {
            IActionResult rv = RedirectToAction("Index", "Client");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                try
                {
                    rv = View(RestaurantManager.GetRestaurants());
                }
                catch { }
            }
            return rv;
        }
    }
}
