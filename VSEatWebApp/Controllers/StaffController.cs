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
    public class StaffController : Controller
    {
        private IStaffManager StaffManager { get; }
        private ILocaliteManager LocaliteManager { get; }

        public StaffController(IStaffManager StaffManager, ILocaliteManager LocaliteManager)
        {
            this.StaffManager = StaffManager;
            this.LocaliteManager = LocaliteManager;
        }

        public IActionResult Index(SimpleUtilisateurVM suVM)
        {
            IActionResult rv = RedirectToAction("Login");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                var staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                if (staff != null)
                {
                    suVM.Nom = staff.Nom;
                    suVM.Prenom = staff.Prenom;
                }
                rv = View(suVM);
            }
            return rv;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            IActionResult rv = View(loginVM);
            if (ModelState.IsValid)
            {
                var staff = StaffManager.GetStaff(loginVM.Mail, loginVM.Password);
                if (staff != null)
                {
                    HttpContext.Session.Clear(); //déconnecter le client s'il était connecté
                    HttpContext.Session.SetInt32("staID", staff.ID);
                    rv = RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Mail ou mot de passe invalide.");
                }
            }
            return rv;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffVM staff)
        {

            return View(staff);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StaffVM staff)
        {

            return View(staff);
        }
    }
}
