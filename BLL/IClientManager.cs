using DTO;

namespace BLL
{
    interface IClientManager
    {
        Client AddClient(string Nom, string Prenom, string Mail, string Password, string Adresse, Localite Localite);
        Client AddClient(string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse, Localite Localite);
    }
}
