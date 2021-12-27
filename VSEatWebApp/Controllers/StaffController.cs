using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VSEatWebApp.Models;

namespace VSEatWebApp.Controllers
{
    /// <summary>
    /// Classe controller pour tout ce qui concerne le staff.
    /// </summary>
    public class StaffController : Controller
    {
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes staff.
        /// </summary>
        private IStaffManager StaffManager { get; }
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes localités.
        /// </summary>
        private ILocaliteManager LocaliteManager { get; }
        /// <summary>
        /// Propriété "raccourci" pour récupérer toutes les localités.
        /// </summary>
        private Localite[] Localites
        {
            get
            {
                Localite[] rv = new Localite[] { };
                try
                {
                    rv = LocaliteManager.GetLocalites();
                }
                catch { }
                return rv;
            }
        }
        /// <summary>
        /// Constructeur du StaffController.
        /// </summary>
        /// <param name="StaffManager">Instance du StaffManager.</param>
        /// <param name="LocaliteManager">Instance du LocaliteManager.</param>
        public StaffController(IStaffManager StaffManager, ILocaliteManager LocaliteManager)
        {
            this.StaffManager = StaffManager;
            this.LocaliteManager = LocaliteManager;
        }
        /// <summary>
        /// Action affichant l'accueil du staff.
        /// </summary>
        /// <param name="suVM">Objet contenant le nom et le prénom de l'utilisateur connecté.</param>
        /// <returns>Vue accueil du staff.</returns>
        public IActionResult Index(SimpleUtilisateurVM suVM)
        {
            IActionResult rv = RedirectToAction("Login", "Staff");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                try
                {
                    Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                    if (staff != null)
                    {
                        suVM.Nom = staff.Nom;
                        suVM.Prenom = staff.Prenom;
                    }
                    rv = View(suVM);
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action affichant la page de connexion staff.
        /// </summary>
        /// <returns>Vue login du staff.</returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Action validant la connexion du staff.
        /// </summary>
        /// <param name="loginVM">Objet contenant les identifiants du staff.</param>
        /// <returns>Vue d'accueil si le staff est connecté.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            IActionResult rv = View(loginVM);
            if (ModelState.IsValid)
            {
                try
                {
                    Staff staff = StaffManager.GetStaff(loginVM.Mail, loginVM.Password);
                    if (staff != null)
                    {
                        HttpContext.Session.Clear(); //déconnecter le client s'il était connecté
                        HttpContext.Session.SetInt32("staID", staff.ID);
                        rv = RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Mail / mot de passe invalide ou compte désactivé.");
                    }
                }
                catch (ConnectionException e)
                {
                    ModelState.AddModelError(string.Empty, e.Details);
                }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant vers la page de création d'un compte staff.
        /// </summary>
        /// <returns>Vue de création d'un compte staff.</returns>
        public IActionResult Create()
        {
            StaffVM staffVM = new StaffVM() { AllLocalites = Localites };
            return View(staffVM);
        }
        /// <summary>
        /// Action validant la création d'un compte staff.
        /// </summary>
        /// <param name="staffVM">Objet contenant les informations du nouveau compte.</param>
        /// <returns>Vue d'accueil du staff si le compte est créé.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffVM staffVM)
        {
            staffVM.AllLocalites = LocaliteManager.GetLocalites();
            IActionResult rv = View(staffVM);
            if (ModelState.IsValid)
            {
                try
                {
                    if (StaffManager.IsMailAvailable(staffVM.Mail))
                    {
                        Staff staff = StaffManager.AddStaff(staffVM.Nom, staffVM.Prenom, staffVM.Telephone, staffVM.Mail, staffVM.Password, LocaliteManager.GetLocalites(staffVM.LocaliteIDs.ToArray()));
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Adresse email déjà utilisée.");
                    }
                }
                catch (ConnectionException e)
                {
                    ModelState.AddModelError(string.Empty, e.Details);
                }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant vers la modification des informations personnelles du staff.
        /// </summary>
        /// <returns>Vue de modification des informations du staff.</returns>
        public IActionResult Edit()
        {
            IActionResult rv = RedirectToAction("Index", "Home");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                try
                {
                    Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                    StaffVM staffVM = new StaffVM() { AllLocalites = Localites };
                    if (staff != null)
                    {
                        staffVM.Nom = staff.Nom;
                        staffVM.Prenom = staff.Prenom;
                        staffVM.Telephone = staff.Telephone;
                        staffVM.Mail = staff.Mail;
                        staffVM.Password = staff.Password;
                        List<int> locIds = new List<int>();
                        foreach (Localite localite in staff.Localites)
                        {
                            locIds.Add(localite.ID);
                        }
                        staffVM.LocaliteIDs = locIds;
                    }
                    rv = View(staffVM);
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action validant la modification des informations du staff.
        /// </summary>
        /// <param name="staffVM">Objet contenant toutes les informations du staff.</param>
        /// <returns>Vue d'accueil du staff si les modifications sont validées.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StaffVM staffVM)
        {
            staffVM.AllLocalites = LocaliteManager.GetLocalites();
            IActionResult rv = View(staffVM);
            if (ModelState.IsValid)
            {
                try
                {
                    Staff staff = new Staff(HttpContext.Session.GetInt32("staID").Value, staffVM.Nom, staffVM.Prenom, staffVM.Telephone, staffVM.Mail, staffVM.Password, LocaliteManager.GetLocalites(staffVM.LocaliteIDs.ToArray()), true);
                    StaffManager.UpdateStaff(staff);
                    rv = RedirectToAction("Index", "Staff");
                }
                catch (ConnectionException e)
                {
                    ModelState.AddModelError(string.Empty, e.Details);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Impossible de mettre à jour les données staff.");
            }
            return rv;
        }
        /// <summary>
        /// Action permettant de désactiver un compte staff.
        /// </summary>
        /// <returns>Page d'accueil du site si le compte a bien été désactivé.</returns>
        public IActionResult Disable()
        {
            IActionResult rv = RedirectToAction("Edit", "Staff");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                try
                {
                    Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                    if (staff != null)
                    {
                        StaffManager.DisableStaff(staff);
                        HttpContext.Session.Clear();
                        rv = RedirectToAction("Index", "Home");
                    }
                }
                catch { }
            }
            return rv;
        }
    }
}
