using DTO;

namespace DAL
{
    /// <summary>
    /// Interface permettant de récupérer ou modifier des informations de la table Client.
    /// </summary>
    public interface IClientDB
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
        /// <returns>Objet de type client contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Client GetClient(string Mail, string Password);
        /// <summary>
        /// Méthode permettant d'ajouter un client.
        /// </summary>
        /// <param name="Client">Nouveau client.</param>
        /// <returns>Client nouvellement inséré dans la DB, avec l'idantifiant unique généré par la DB.</returns>
        Client AddClient(Client Client);
        /// <summary>
        /// Méthode permettant de mettre à jour un client.
        /// </summary>
        /// <param name="Client">Client à mettre à jour.</param>
        void UpdateClient(Client Client);
        /// <summary>
        /// Méthode permettant de savoir si une adresse mail est déjà utilisée pour un compte staff.
        /// </summary>
        /// <param name="Mail">Adresse mail à vérifier.</param>
        /// <returns>True si l'adresse mail est disponible, false autrement.</returns>
        bool IsMailAvailable(string Mail);
    }
}
