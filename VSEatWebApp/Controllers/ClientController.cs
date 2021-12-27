using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VSEatWebApp.Models;

namespace VSEatWebApp.Controllers
{
    /// <summary>
    /// Classe controller pour tout ce qui concerne les clients.
    /// </summary>
    public class ClientController : Controller
    {
        /// <summary>
        /// Objet permettant d'intéragir avec les méthodes client.
        /// </summary>
        private IClientManager ClientManager { get; }
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
        /// Constructeur du ClientController.
        /// </summary>
        /// <param name="ClientManager">Instance du ClientManager.</param>
        /// <param name="LocaliteManager">Instance du LocaliteManager.</param>
        public ClientController(IClientManager ClientManager, ILocaliteManager LocaliteManager)
        {
            this.ClientManager = ClientManager;
            this.LocaliteManager = LocaliteManager;
        }
        /// <summary>
        /// Action affichant l'accueil du client.
        /// </summary>
        /// <param name="suVM">Objet contenant le nom et le prénom de l'utilisateur.</param>
        /// <returns>Vue accueil du client.</returns>
        public IActionResult Index(SimpleUtilisateurVM suVM)
        {
            IActionResult rv = RedirectToAction("Login", "Client");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                try
                {
                    Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                    if (client != null)
                    {
                        suVM.Nom = client.Nom;
                        suVM.Prenom = client.Prenom;
                    }
                    rv = View(suVM);
                }
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action affichant la page de connexion client.
        /// </summary>
        /// <returns>Vue login du client.</returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// Action validant la connexion du client.
        /// </summary>
        /// <param name="loginVM">Objet contenant les identifiants du client.</param>
        /// <returns>Vue d'accueil si le client est connecté.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            IActionResult rv = View(loginVM);
            if (ModelState.IsValid)
            {
                try
                {
                    Client client = ClientManager.GetClient(loginVM.Mail, loginVM.Password);
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
                catch (ConnectionException e)
                {
                    ModelState.AddModelError(string.Empty, e.Details);
                }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant vers la page de création d'un compte client.
        /// </summary>
        /// <returns>Vue de création d'un compte client.</returns>
        public IActionResult Create()
        {
            ClientVM clientVM = new ClientVM() { AllLocalites = Localites };
            return View(clientVM);
        }
        /// <summary>
        /// Action validant la création d'un compte client.
        /// </summary>
        /// <param name="clientVM">Objet contenant les informations du nouveau compte client.</param>
        /// <returns>Vue d'accueil du client si le compte est créé.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientVM clientVM)
        {
            clientVM.AllLocalites = Localites;
            IActionResult rv = View(clientVM);
            if (ModelState.IsValid)
            {
                try
                {
                    Client client = ClientManager.AddClient(clientVM.Nom, clientVM.Prenom, clientVM.Telephone, clientVM.Mail, clientVM.Password, clientVM.Adresse, LocaliteManager.GetLocalite(clientVM.LocaliteID));
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
                catch (ConnectionException e)
                {
                    ModelState.AddModelError(string.Empty, e.Details);
                }
            }
            return rv;
        }
        /// <summary>
        /// Action redirigeant vers la modification des informations personnelles du client.
        /// </summary>
        /// <returns>Vue de modification des informations du client.</returns>
        public IActionResult Edit()
        {
            IActionResult rv = RedirectToAction("Index", "Home");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                try
                {
                    Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                    ClientVM clientVM = new ClientVM() { AllLocalites = Localites };
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
                catch { }
            }
            return rv;
        }
        /// <summary>
        /// Action validant la modification des informations du client.
        /// </summary>
        /// <param name="clientVM">Objet contenant toutes les informations du client.</param>
        /// <returns>Vue d'accueil du client si les modifications sont validées.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientVM clientVM)
        {
            clientVM.AllLocalites = Localites;
            IActionResult rv = View(clientVM);
            if (ModelState.IsValid)
            {
                try
                {
                    Client client = new Client(HttpContext.Session.GetInt32("cliID").Value, LocaliteManager.GetLocalite(clientVM.LocaliteID), clientVM.Nom, clientVM.Prenom, clientVM.Telephone, clientVM.Mail, clientVM.Password, clientVM.Adresse, true);
                    ClientManager.UpdateClient(client);
                    rv = RedirectToAction("Index", "Client");
                }
                catch (ConnectionException e)
                {
                    ModelState.AddModelError(string.Empty, e.Details);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Impossible de mettre à jour les données client.");
            }
            return rv;
        }
        /// <summary>
        /// Action permettant de désactiver un compte client.
        /// </summary>
        /// <returns>Page d'accueil du site si le compte a bien été désactivé.</returns>
        public IActionResult Disable()
        {
            IActionResult rv = RedirectToAction("Edit", "Client");
            if (HttpContext.Session.GetInt32("cliID").HasValue)
            {
                try
                {
                    Client client = ClientManager.GetClient(HttpContext.Session.GetInt32("cliID").Value);
                    if (client != null)
                    {
                        ClientManager.DisableClient(client);
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
