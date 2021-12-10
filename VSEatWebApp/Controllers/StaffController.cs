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
                DTO.Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
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
                DTO.Staff staff = StaffManager.GetStaff(loginVM.Mail, loginVM.Password);
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
            StaffVM staffVM = new StaffVM() { AllLocalites = LocaliteManager.GetLocalites() };
            return View(staffVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffVM staffVM)
        {
            staffVM.AllLocalites = LocaliteManager.GetLocalites();
            IActionResult rv = View(staffVM);
            if (ModelState.IsValid)
            {
                DTO.Staff staff = StaffManager.AddStaff(staffVM.Nom, staffVM.Prenom, staffVM.Telephone, staffVM.Mail, staffVM.Password, LocaliteManager.GetLocalites(staffVM.LocaliteIDs.ToArray()));
                if (staff != null)
                {
                    if (staff.ID >= 0)
                    {
                        HttpContext.Session.Clear();
                        HttpContext.Session.SetInt32("staID", staff.ID);
                        rv = RedirectToAction("Index", "Staff");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Impossible de créer le compte staff.");
                }
            }
            return rv;
        }

        public IActionResult Edit()
        {
            IActionResult rv = RedirectToAction("Index", "Home");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                DTO.Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                StaffVM staffVM = new StaffVM() { AllLocalites = LocaliteManager.GetLocalites() };
                if (staff != null)
                {
                    staffVM.Nom = staff.Nom;
                    staffVM.Prenom = staff.Prenom;
                    staffVM.Telephone = staff.Telephone;
                    staffVM.Mail = staff.Mail;
                    staffVM.Password = staff.Password;
                    List<int> locIds = new List<int>();
                    foreach (DTO.Localite localite in staff.Localites)
                    {
                        locIds.Add(localite.ID);
                    }
                    staffVM.LocaliteIDs = locIds;
                }
                rv = View(staffVM);
            }
            return rv;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StaffVM staffVM)
        {
            staffVM.AllLocalites = LocaliteManager.GetLocalites();
            IActionResult rv = View(staffVM);
            if (ModelState.IsValid)
            {
                DTO.Staff staff = new DTO.Staff(HttpContext.Session.GetInt32("staID").Value, staffVM.Nom, staffVM.Prenom, staffVM.Telephone, staffVM.Mail, staffVM.Password, LocaliteManager.GetLocalites(staffVM.LocaliteIDs.ToArray()), true);
                StaffManager.UpdateStaff(staff);
                rv = RedirectToAction("Index", "Staff");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Impossible de mettre à jour les données staff.");
            }
            return rv;
        }

        public IActionResult Disable()
        {
            IActionResult rv = RedirectToAction("Edit", "Staff");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                DTO.Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                if (staff != null)
                {
                    StaffManager.DisableStaff(staff);
                    HttpContext.Session.Clear();
                    rv = RedirectToAction("Index", "Home");
                }
            }
            return rv;
        }
    }
}
