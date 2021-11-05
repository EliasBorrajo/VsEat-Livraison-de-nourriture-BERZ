using DTO;

namespace DAL
{
    /// <summary>
    /// Interface permettant de récupérer ou modifier des informations de la table Plat.
    /// </summary>
    public interface IPlatDB
    {
        /// <summary>
        /// Méthode permettant de récupérer un plat par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique du plat.</param>
        /// <returns>Objet de type Plat contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Plat GetPlat(int ID);
        /// <summary>
        /// Méthode permettant de récupérer les plats d'un restaurant.
        /// </summary>
        /// <param name="ID">Identifiant unique du restaurant.</param>
        /// <returns>Tableau de Plat contenant les informations enregistreées dans la BD.</returns>
        Plat[] GetRestaurantPlats(int ID);
        /// <summary>
        /// Méthode permettant de récupérer les plats d'une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <returns>Tableau de CommandePlat contenant les informations enregistrées dans la BD.</returns>
        CommandePlat[] GetCommandePlats(int ID);
        /// <summary>
        /// Méthode permettant de définir les plats d'une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <param name="Plats">Tableau de CommandePlat contenant les plats de la commande ainsi que leur quantité.</param>
        void SetCommandePlats(int ID, CommandePlat[] Plats);
    }
}
