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
    public class ClientController : Controller
    {
        private IClientManager ClientManager { get; }
        private ILocaliteManager LocaliteManager { get; }

        public ClientController(IClientManager ClientManager, ILocaliteManager LocaliteManager)
        {
            this.ClientManager = ClientManager;
            this.LocaliteManager = LocaliteManager;
        }

        public IActionResult Index(SimpleUtilisateurVM suVM)
        {
            IActionResult rv = RedirectToAction("Login");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                var client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                if (client != null)
                {
                    suVM.Nom = client.Nom;
                    suVM.Prenom = client.Prenom;
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
                var client = ClientManager.GetClient(loginVM.Mail, loginVM.Password);
                if (client != null)
                {
                    HttpContext.Session.Clear(); // déconnecter le staff s'il était connecté
                    HttpContext.Session.SetInt32("cliID", client.ID);
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
            var localites = LocaliteManager.GetLocalites();
            LocaliteVM[] localiteVMs = new LocaliteVM[localites.Length];
            for (int i = 0; i < localites.Length; i++)
            {
                localiteVMs[i] = new LocaliteVM() { Nom = localites[i].Nom, NPA = localites[i].NPA };
            }
            ClientVM clientVM = new ClientVM() { AllLocalites = localiteVMs };
            return View(clientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientVM clientVM)
        {
            IActionResult rv = View(clientVM);
            if (ModelState.IsValid)
            {
                var localites = LocaliteManager.GetLocalites();
                var client = ClientManager.AddClient(clientVM.Nom, clientVM.Prenom, clientVM.Telephone, clientVM.Mail, clientVM.Password, clientVM.Adresse, localites.FirstOrDefault(x => x.NPA == clientVM.Localite.NPA));
                if (client.ID >= 0)
                {
                    rv = RedirectToAction("Index", "Client");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Impossible de créer le compte client.");
                }
            }
            return rv;
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientVM clientVM)
        {

            return View(clientVM);
        }
    }
}
