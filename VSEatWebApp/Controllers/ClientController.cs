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
            IActionResult rv = RedirectToAction("Login", "Client");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                DTO.Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
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
                DTO.Client client = ClientManager.GetClient(loginVM.Mail, loginVM.Password);
                if (client != null)
                {
                    HttpContext.Session.Clear();
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
            ClientVM clientVM = new ClientVM() { AllLocalites = LocaliteManager.GetLocalites() };
            return View(clientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientVM clientVM)
        {
            clientVM.AllLocalites = LocaliteManager.GetLocalites();
            IActionResult rv = View(clientVM);
            if (ModelState.IsValid)
            {
                DTO.Client client = ClientManager.AddClient(clientVM.Nom, clientVM.Prenom, clientVM.Telephone, clientVM.Mail, clientVM.Password, clientVM.Adresse, LocaliteManager.GetLocalite(clientVM.LocaliteID));
                if (client != null)
                {
                    if (client.ID >= 0)
                    {
                        HttpContext.Session.Clear();
                        HttpContext.Session.SetInt32("cliID", client.ID);
                        rv = RedirectToAction("Index", "Client");
                    }
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
            IActionResult rv = RedirectToAction("Index", "Home");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                DTO.Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                ClientVM clientVM = new ClientVM() { AllLocalites = LocaliteManager.GetLocalites() };
                if (client != null)
                {
                    clientVM.Nom = client.Nom;
                    clientVM.Prenom = client.Prenom;
                    clientVM.Telephone = client.Telephone;
                    clientVM.Mail = client.Mail;
                    clientVM.Password = client.Password;
                    clientVM.Adresse = client.Adresse;
                    clientVM.LocaliteID = client.Localite.ID;
                }
                rv = View(clientVM);
            }
            return rv;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientVM clientVM)
        {
            clientVM.AllLocalites = LocaliteManager.GetLocalites();
            IActionResult rv = View(clientVM);
            if (ModelState.IsValid)
            {
                DTO.Client client = new DTO.Client(HttpContext.Session.GetInt32("cliID").Value, LocaliteManager.GetLocalite(clientVM.LocaliteID), clientVM.Nom, clientVM.Prenom, clientVM.Telephone, clientVM.Mail, clientVM.Password, clientVM.Adresse, true);
                ClientManager.UpdateClient(client);
                rv = RedirectToAction("Index", "Client");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Impossible de mettre à jour les données client.");
            }
            return rv;
        }

        public IActionResult Disable()
        {
            IActionResult rv = RedirectToAction("Edit", "Client");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                DTO.Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                if (client != null)
                {
                    ClientManager.DisableClient(client);
                    HttpContext.Session.Clear();
                    rv = RedirectToAction("Index", "Home");
                }
            }
            return rv;
        }
    }
}
