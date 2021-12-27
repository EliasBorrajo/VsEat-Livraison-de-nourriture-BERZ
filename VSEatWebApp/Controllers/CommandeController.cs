using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VSEatWebApp.Models;

namespace VSEatWebApp.Controllers
{
    /// <summary>
    /// Classe controller pour tout ce qui concerne les commandes.
    /// </summary>
    public class CommandeController : Controller
    {
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes commande.
        /// </summary>
        private ICommandeManager CommandeManager { get; }
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes client.
        /// </summary>
        private IClientManager ClientManager { get; }
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes staff.
        /// </summary>
        private IStaffManager StaffManager { get; }
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes restaurant.
        /// </summary>
        private IRestaurantManager RestaurantManager { get; }
        /// <summary>
        /// Propriété permettant de récupérer les heures de livraison disponibles.
        /// </summary>
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
                first = first.AddSeconds(-first.Second);
                List<DateTime> rv = new List<DateTime>();
                rv.Add(first);
                for (int i = 1; i < 47; i++)
                {
                    rv.Add(first.AddMinutes(15 * i));
                }
                return rv;
            } 
        }
        /// <summary>
        /// Propriété permettant de récupérer le format pour les champs DateTime dans les vues.
        /// </summary>
        private string DateTimeFormat { get { return "HH:mm - dd.MM.yyyy"; } }
        /// <summary>
        /// Constructeur du CommandeController.
        /// </summary>
        /// <param name="CommandeManager">Instance du CommandeManager.</param>
        /// <param name="ClientManager">Instance du ClientManager.</param>
        /// <param name="RestaurantManager">Instance du RestaurantManager.</param>
        /// <param name="StaffManager">Instance du StaffManager.</param>
        public CommandeController(ICommandeManager CommandeManager, IClientManager ClientManager, IRestaurantManager RestaurantManager, IStaffManager StaffManager)
        {
            this.CommandeManager = CommandeManager;
            this.ClientManager = ClientManager;
            this.RestaurantManager = RestaurantManager;
            this.StaffManager = StaffManager;
        }
        /// <summary>
        /// Action affichant la liste des commandes.
        /// </summary>
        /// <param name="status">1 pour les commandes en cours, 0 pour toutes les commandes et -1 pour les commandes terminées.</param>
        /// <returns>Vue liste des commandes.</returns>
        public IActionResult List(int status)
        {
            ViewData["DateTimeFormat"] = DateTimeFormat;
            IActionResult rv = RedirectToAction("Index", "Home");
            Commande[] commandes = null;
            string action = string.Empty;
            bool checkCancel = false;
            bool? orderStatus = status == 0 ? null : status == 1;
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                try
                {
                    Staff staff = StaffManager.GetStaff(HttpContext.Session.GetInt32("staID").Value);
                    if (staff != null)
                    {
                        action = "Valider";
                        commandes = CommandeManager.GetStaffCommandes(staff, orderStatus);
                    }
                }
                catch { }
            }
            else if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                try
                {
                    Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                    if (client != null)
                    {
                        action = "Annuler";
                        checkCancel = true;
                        commandes = CommandeManager.GetClientCommandes(client, orderStatus);
                    }
                }
                catch { }
            }
            if (commandes != null)
            {
                try
                {
                    List<CommandeVM> commandeVMs = new List<CommandeVM>();
                    foreach (Commande commande in commandes)
                    {
                        bool displayAction = CommandeManager.IsEnCours(commande);
                        if (checkCancel)
                        {
                            displayAction = displayAction && CommandeManager.CanBeCancelled(commande);
                        }
                        CommandeVM commandeVM = new CommandeVM();
                        commandeVM.Commande = commande;
                        commandeVM.Restaurant = RestaurantManager.GetRestaurantByCommande(commande);
                        commandeVM.EnCours = CommandeManager.IsEnCours(commande);
                        commandeVM.Action = new CommandeAction() { Action = action, Display = displayAction };
                        commandeVMs.Add(commandeVM);
                    }
                    rv = View(commandeVMs);
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action affichant le détail d'une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <returns>Vue détail d'une commande.</returns>
        public IActionResult Detail(int ID)
        {
            ViewData["DateTimeFormat"] = DateTimeFormat;
            IActionResult rv = RedirectToAction("List", "Commande", new { status = 1 });
            bool clientConnected = HttpContext.Session.GetInt32("cliID").HasValue;
            bool staffConnected = HttpContext.Session.GetInt32("staID").HasValue;
            string action = clientConnected ? "Annuler" : "Valider";
            if (clientConnected || staffConnected)
            {
                try
                {
                    Commande commande = CommandeManager.GetCommande(ID);
                    if (commande != null)
                    {
                        bool displayAction = CommandeManager.IsEnCours(commande);
                        if (clientConnected)
                        {
                            displayAction = displayAction && CommandeManager.CanBeCancelled(commande);
                        }
                        CommandeVM commandeVM = new CommandeVM()
                        {
                            Commande = commande,
                            Restaurant = RestaurantManager.GetRestaurantByCommande(commande),
                            Action = new CommandeAction() { Action = action, Display = displayAction },
                            EnCours = CommandeManager.IsEnCours(commande)
                        };
                        rv = View(commandeVM); ;
                    }
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant sur l'action possible depuis une commande en fonction du type d'utilisateur connecté.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commmande.</param>
        /// <returns>Vue liste ou vue annulation d'une commande.</returns>
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
        /// <summary>
        /// Action confirmant le paiement d'une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <returns>Vue liste des commmandes.</returns>
        public IActionResult Confirm(int ID)
        {
            IActionResult rv = RedirectToAction("List", "Commande", new { status = 1 });
            if (HttpContext.Session.GetInt32("staID").HasValue)
            {
                try
                {
                    Commande commande = CommandeManager.GetCommande(ID);
                    if (commande != null)
                    {
                        commande = CommandeManager.ValidatePayment(commande);
                        if (commande.HeurePaiement > commande.Heure)
                        {
                            rv = RedirectToAction("List", "Commande", new { status = 1 });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Impossible de valider la commande.");
                        }
                    }
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant sur la vue annulation d'une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <returns>Vue d'annulation de la commande.</returns>
        public IActionResult Cancel(int ID)
        {
            ViewData["DateTimeFormat"] = DateTimeFormat;
            IActionResult rv = RedirectToAction("List", "Commande", new { status = 1 });
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                try
                {
                    Commande commande = CommandeManager.GetCommande(ID);
                    if (commande != null)
                    {
                        CancelCommandeVM commandeVM = new CancelCommandeVM()
                        {
                            CommandeID = commande.ID,
                            Commande = commande,
                            Restaurant = RestaurantManager.GetRestaurantByCommande(commande),
                            EnCours = CommandeManager.IsEnCours(commande)
                        };
                        rv = View(commandeVM);
                    }
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action validant l'annulation d'une commande.
        /// </summary>
        /// <param name="commandeVM">Objet contenant les informations nécessaires à l'annulation d'une commande.</param>
        /// <returns>Vue liste des commandes.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(CancelCommandeVM commandeVM)
        {
            ViewData["DateTimeFormat"] = DateTimeFormat;
            try
            {
                commandeVM.Commande = CommandeManager.GetCommande(commandeVM.CommandeID);
                commandeVM.Restaurant = RestaurantManager.GetRestaurantByCommande(commandeVM.Commande);
            }
            catch { }
            IActionResult rv = View(commandeVM);
            if (ModelState.IsValid)
            {
                if (commandeVM.ControleID != commandeVM.CommandeID)
                {
                    ModelState.AddModelError(string.Empty, "Veuillez vérifier le numéro de la commmande.");
                }
                else
                {
                    try
                    {
                        Commande commande = CommandeManager.CancelCommande(commandeVM.ControleID, commandeVM.Nom, commandeVM.Prenom);
                        if (commande != null && commande.Annule)
                        {
                            rv = RedirectToAction("Detail", "Commande", new { id = commande.ID });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Impossible d'annuler la commande. Veuillez vérifier votre nom et prénom.");
                        }
                    }
                    catch (ConnectionException e)
                    {
                        ModelState.AddModelError(string.Empty, e.Details);
                    }
                }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant vers la vue ajout d'une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique du restaurant.</param>
        /// <returns>Vue ajout d'une commande.</returns>
        public IActionResult Add(int ID)
        {
            try
            {
                Restaurant restaurant = RestaurantManager.GetRestaurant(ID);
                Dictionary<int, int> plats = new Dictionary<int, int>();
                foreach (Plat plat in restaurant.Plats)
                {
                    plats.Add(plat.ID, 0);
                }
                AddCommandeVM commandeVM = new AddCommandeVM()
                {
                    RestaurantID = ID,
                    Restaurant = restaurant,
                    HeuresPossibles = HeuresLivraisonDisponibles,
                    PlatsQuantites = plats
                };
                return View(commandeVM);
            }
            catch
            {
                return RedirectToAction("Index", "Restaurant");
            }
        }
        /// <summary>
        /// Action validant l'ajout d'une commande.
        /// </summary>
        /// <param name="commandeVM">Objet contenant toutes les informations nécessaires à la création de la commande.</param>
        /// <returns>Vue détail de la commande si elle a pu être ajoutée.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddCommandeVM commandeVM)
        {
            IActionResult rv = View(commandeVM);
            try
            {
                Restaurant restaurant = RestaurantManager.GetRestaurant(commandeVM.RestaurantID);
                commandeVM.Restaurant = restaurant;
                commandeVM.HeuresPossibles = HeuresLivraisonDisponibles;
                if (ModelState.IsValid)
                {
                    Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                    List<CommandePlat> plats = new List<CommandePlat>();
                    foreach (Plat plat in restaurant.Plats)
                    {
                        if (commandeVM.PlatsQuantites.ContainsKey(plat.ID))
                        {
                            if (commandeVM.PlatsQuantites[plat.ID] > 0)
                            {
                                plats.Add(new CommandePlat(plat, commandeVM.PlatsQuantites[plat.ID]));
                            }
                        }
                    }
                    if (plats.Count > 0)
                    {
                        Commande commande = CommandeManager.AddCommande(client, commandeVM.Restaurant, plats.ToArray(), commandeVM.HeureLivraison);
                        if (commande != null)
                        {
                            rv = RedirectToAction("Detail", "Commande", new { id = commande.ID });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, $"Aucun staff disponible à {commandeVM.Restaurant.Localite.Nom} pour l'heure de livraison sélectionnée.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Impossible de passer une commande sans plat.");
                    }
                }
            }
            catch (ConnectionException e)
            {
                ModelState.AddModelError(string.Empty, e.Details);
            }
            return rv;
        }
    }
}
