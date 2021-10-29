using DTO;

namespace BLL
{
    public interface IClientManager
    {
        Client GetClient(string Mail, string Password);
        Client AddClient(string Nom, string Prenom, string Mail, string Password, string Adresse, Localite Localite);
        Client AddClient(string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse, Localite Localite);
        void UpdateClient(Client Client);
        void DeleteClient(Client Client);
    }
}
