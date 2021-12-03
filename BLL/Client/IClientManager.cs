using DTO;

namespace BLL
{
    /// <summary>
    /// Interface permettant de gérer tout ce qui concerne les clients.
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        /// Méthode permettant de récupérer un client par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique du client.</param>
        /// <returns>Objet de type Client contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Client GetClient(int ID);
        /// <summary>
        /// Méthode permettant de récupérer un client par son adresse mail et son mot de passe.
        /// </summary>
        /// <param name="Mail">Mail du client.</param>
        /// <param name="Password">Mot de passe du client.</param>
        /// <returns>Objet de type Client contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Client GetClient(string Mail, string Password);
        /// <summary>
        /// Méthode permettant d'ajouter un client.
        /// </summary>
        /// <param name="Nom">Nom du client.</param>
        /// <param name="Prenom">Prénom du client.</param>
        /// <param name="Mail">Mail du client.</param>
        /// <param name="Password">Mot de passe du client.</param>
        /// <param name="Adresse">Adresse du client.</param>
        /// <param name="Localite">Localité du client.</param>
        /// <returns>Client nouvellement ajouté, avec l'identifiant unique généré par la DB.</returns>
        Client AddClient(string Nom, string Prenom, string Mail, string Password, string Adresse, Localite Localite);
        /// <summary>
        /// Méthode permettant d'ajouter un client.
        /// </summary>
        /// <param name="Nom">Nom du client.</param>
        /// <param name="Prenom">Prénom du client.</param>
        /// <param name="Telephone">Téléphone du client.</param>
        /// <param name="Mail">Mail du client.</param>
        /// <param name="Password">Mot de passe du client.</param>
        /// <param name="Adresse">Adresse du client.</param>
        /// <param name="Localite">Localité du client.</param>
        /// <returns>Client nouvellement ajouté, avec l'identifiant unique généré par la DB.</returns>
        Client AddClient(string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse, Localite Localite);
        /// <summary>
        /// Méthode permettant de mettre à jour un client.
        /// </summary>
        /// <param name="Client">Client contenant les informations à mettre à jour.</param>
        void UpdateClient(Client Client);
        /// <summary>
        /// Méthode permettant de désactiver un client.
        /// </summary>
        /// <param name="Client">Client à désactiver.</param>
        void DisableClient(Client Client);
    }
}
