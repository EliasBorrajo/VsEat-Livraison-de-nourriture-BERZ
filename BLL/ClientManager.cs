using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class ClientManager : IClientManager
    {
        private IClientDB ClientDB { get; }
        private ICommandeDB CommandeDB { get; }

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
            Client newClient = new Client(-1, Localite, Nom, Prenom, Telephone, Mail, Password, Adresse);
            return ClientDB.AddClient(newClient);
        }
        public Client GetClient(string Mail, string Password)
        {
            return ClientDB.GetClient(Mail, Password);
        }
        public void UpdateClient(Client Client)
        {
            ClientDB.UpdateClient(Client);
        }
        public void DeleteClient(Client Client)
        {
            CommandeDB.DeleteCommandes(Client);
            ClientDB.DeleteClient(Client);
        }
    }
}
