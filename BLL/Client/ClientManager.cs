using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /// <summary>
    /// Classe qui implémente l'interface IClientManager, permettant la gestion de tout ce qui concerne les clients.
    /// </summary>
    public class ClientManager : IClientManager
    {
        /// <summary>
        /// Objet permettant d'interagir avec la table Client.
        /// </summary>
        private IClientDB ClientDB { get; }
        /// <summary>
        /// Objet permettant d'interagir avec la table Commande.
        /// </summary>
        private ICommandeDB CommandeDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet ClientManager.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public ClientManager(IConfiguration Configuration)
        {
            ClientDB = new ClientDB(Configuration);
            CommandeDB = new CommandeDB(Configuration);
        }
        public Client AddClient(string Nom, string Prenom, string Mail, string Password, string Adresse, Localite Localite)
        {
            return AddClient(Nom, Prenom, string.Empty, Mail, Password, Adresse, Localite);
        }
        public Client AddClient(string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse, Localite Localite)
        {
            Client newClient = new Client(-1, Localite, Nom, Prenom, Telephone, Mail, Password, Adresse, true);
            return ClientDB.AddClient(newClient);
        }
        public Client GetClient(int ID)
        {
            return ClientDB.GetClient(ID);
        }
        public Client GetClient(string Mail, string Password)
        {
            return ClientDB.GetClient(Mail, Password);
        }
        public void UpdateClient(Client Client)
        {
            ClientDB.UpdateClient(Client);
        }
        public void DisableClient(Client Client)
        {
            Client.Status = false;
            UpdateClient(Client);
        }
    }
}
