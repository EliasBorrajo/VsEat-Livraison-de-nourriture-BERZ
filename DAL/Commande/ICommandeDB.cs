using DTO;

namespace DAL
{
    /// <summary>
    /// Interface permettant de récupérer ou modifier des informations de la table Commande.
    /// </summary>
    public interface ICommandeDB
    {
        /// <summary>
        /// Méthode permettant de récupérer une commande par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <returns>Objet de type Commande contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Commande GetCommande(int ID);
        /// <summary>
        /// Méthode permettant de récupérer les commandes dont un staff est responsable.
        /// </summary>
        /// <param name="Staff">Staff dont on souhaite récupérer les commandes.</param>
        /// <returns>Tableau de Commande contenant les commandes dont le staff est responsable.</returns>
        Commande[] GetStaffCommandes(Staff Staff);
        /// <summary>
        /// Méthode permettant de récupérer les commandes passées par un client.
        /// </summary>
        /// <param name="Client">Client dont on souhaite récupérer les commandes.</param>
        /// <returns>Tableau de Commande contenant les commandes passées par le client.</returns>
        Commande[] GetClientCommandes(Client Client);
        /// <summary>
        /// Méthode permettant d'ajouter une nouvelle commande.
        /// </summary>
        /// <param name="Commande">Nouvelle commande.</param>
        /// <returns>Commande nouvellement insérée dans la DB, avec l'identifiant unique généré par la DB.</returns>
        Commande AddCommande(Commande Commande);
        /// <summary>
        /// Méthode permettant de mettre à jour une commande.
        /// </summary>
        /// <param name="Commande">Commande à mettre à jour.</param>
        void UpdateCommande(Commande Commande);
    }
}
