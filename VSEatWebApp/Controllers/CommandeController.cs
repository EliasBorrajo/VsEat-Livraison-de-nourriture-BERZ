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
    public class CommandeController : Controller
    {
        private ICommandeManager CommandeManager { get; }
        private IClientManager ClientManager { get; }
        private IStaffManager StaffManager { get; }
        private IRestaurantManager RestaurantManager { get; }

        private List<DateTime> HeuresLivraisonDisponibles
        {
            get
            {
                int addMin = 0;
                if (DateTime.Now.Minute <= 15)
                {
                    addMin = 15 - DateTime.Now.Minute;
                }
                else if (DateTime.Now.Minute <= 30)
                {
                    addMin = 30 - DateTime.Now.Minute;
                }
                else if (DateTime.Now.Minute <= 45)
                {
                    addMin = 45 - DateTime.Now.Minute;
                }
                else
                {
                    addMin = 60 - DateTime.Now.Minute;
                }
                DateTime first = DateTime.Now.AddMinutes(addMin);
                List<DateTime> rv = new List<DateTime>();
                rv.Add(first);
                for (int i = 1; i < 47; i++)
                {
                    rv.Add(first.AddMinutes(15 * i));
                }
                return rv;
            } 
        }

        public CommandeController(ICommandeManager CommandeManager, IClientManager ClientManager, IRestaurantManager RestaurantManager, IStaffManager StaffManager)
        {
            this.CommandeManager = CommandeManager;
            this.ClientManager = ClientManager;
            this.RestaurantManager = RestaurantManager;
            this.StaffManager = StaffManager;
        }

        public IActionResult List(int status)
        {
            IActionResult rv = RedirectToAction("Index", "Home");
            DTO.Commande[] commandes = null;
            string action = string.Empty;
            bool? orderStatus = status == 0 ? null : status == 1;
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                DTO.Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                if (staff != null)
                {
                    action = "Valider";
                    commandes = CommandeManager.GetStaffCommandes(staff, orderStatus);
                }
            }
            else if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                DTO.Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                if (client != null)
                {
                    action = "Annuler";
                    commandes = CommandeManager.GetClientCommandes(client, orderStatus);
                }
            }
            if (commandes != null)
            {
                List<SimpleCommandeVM> commandeVMs = new List<SimpleCommandeVM>();
                foreach (DTO.Commande commande in commandes)
                {
                    commandeVMs.Add(new SimpleCommandeVM()
                    {
                        Commande = commande,
                        Restaurant = RestaurantManager.GetRestaurantByPlat(commande.Plats[0]),
                        EnCours = commande.HeurePaiement < commande.Heure && !commande.Annule,
                        Action = action
                    });
                }
                rv = View(commandeVMs);
            }
            return rv;
        }

        public IActionResult Detail(int ID)
        {
            IActionResult rv = RedirectToAction("List", "Commande", new { id = 1 });
            bool clientConnected = HttpContext.Session.GetInt32("cliID").HasValue;
            bool staffConnected = HttpContext.Session.GetInt32("staID").HasValue;
            if (clientConnected || staffConnected)
            {
                DTO.Commande commande = CommandeManager.GetCommande(ID);
                if (commande != null)
                {
                    CommandeVM commandeVM = new CommandeVM() { Commande = commande, Action = clientConnected ? "Annuler" : "Valider" };
                    rv = View(commandeVM); ;
                }
            }
            return rv;
        }

        public IActionResult Action(int ID)
        {
            IActionResult rv = RedirectToAction("List", "Commande");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                rv = RedirectToAction("Confirm", "Commande", new { id = ID });
            }
            else if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                rv = RedirectToAction("Cancel", "Commande", new { id = ID });
            }
            return rv;
        }

        public IActionResult Confirm(int ID)
        {
            IActionResult rv = RedirectToAction("List", "Commande");
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                DTO.Commande commande = CommandeManager.GetCommande(ID);
                if (commande != null)
                {
                    commande = CommandeManager.ValidatePayment(commande);
                    if (commande.HeurePaiement > DateTime.MinValue)
                    {
                        rv = RedirectToAction("StaffCommande", "Commande");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Impossible de valider la commande.");
                    }
                }
            }
            return rv;
        }

        public IActionResult Cancel(int ID)
        {
            IActionResult rv = RedirectToAction("List", "Commande");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                DTO.Commande commande = CommandeManager.GetCommande(ID);
                if (commande != null)
                {
                    CancelCommandeVM commandeVM = new CancelCommandeVM() { Commande = commande };
                    rv = View(commandeVM);
                }
            }
            return rv;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(CancelCommandeVM commandeVM)
        {
            IActionResult rv = View(commandeVM);
            if (ModelState.IsValid)
            {
                DTO.Commande commande = CommandeManager.CancelCommande(commandeVM.CommandeID, commandeVM.Nom, commandeVM.Prenom);
                if (commande != null && commande.Annule)
                {
                    rv = RedirectToAction("List", "Commande");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Impossible d'annuler la commande.");
                }
            }
            return rv;
        }

        public IActionResult Add(int ID)
        {
            DTO.Restaurant restaurant = RestaurantManager.GetRestaurant(ID);
            Dictionary<int, int> plats = new Dictionary<int, int>();
            foreach (DTO.Plat plat in restaurant.Plats)
            {
                plats.Add(plat.ID, 0);
            }
            AddCommandeVM commandeVM = new AddCommandeVM() { RestaurantID = ID, Restaurant = restaurant, HeuresPossibles = HeuresLivraisonDisponibles, PlatsQuantites = plats };
            return View(commandeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddCommandeVM commandeVM)
        {
            DTO.Restaurant restaurant = RestaurantManager.GetRestaurant(commandeVM.RestaurantID);
            if (restaurant != null)
            {
                commandeVM.Restaurant = restaurant;
            }
            IActionResult rv = View(commandeVM);
            if (ModelState.IsValid)
            {
                DTO.Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                List<DTO.CommandePlat> plats = new List<DTO.CommandePlat>();
                foreach (DTO.Plat plat in restaurant.Plats)
                {
                    if (commandeVM.PlatsQuantites.ContainsKey(plat.ID))
                    {
                        if (commandeVM.PlatsQuantites[plat.ID] > 0)
                        {
                            plats.Add(new DTO.CommandePlat(plat, commandeVM.PlatsQuantites[plat.ID]));
                        }
                    }
                }
                DTO.Commande commande = CommandeManager.AddCommande(client, commandeVM.Restaurant, plats.ToArray(), commandeVM.HeureLivraison);
                if (commande != null)
                {

                    rv = RedirectToAction("NEXTSTEP");
                }
            }
            else
            {

            }
            return rv;
        }
    }
}
