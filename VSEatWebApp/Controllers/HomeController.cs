using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
        /// <summary>
        /// Action affichant l'accueil du site ou redirigeant sur l'accueil de l'utilisateur connecté.
        /// </summary>
        /// <returns>Vue accueil du site, du staff ou du client.</returns>
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
        /// <summary>
        /// Action permettant d'accéder aux informations du compte connecté.
        /// </summary>
        /// <returns>Vue "mon compte" du client ou du staff.</returns>
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
        /// <summary>
        /// Action permettant de déconnecter l'utilisateur connecté.
        /// </summary>
        /// <returns>Vue accueil du site.</returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Action permettant de retourner la vue confidentialité.
        /// </summary>
        /// <returns>Vue confidentialité</returns>
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
