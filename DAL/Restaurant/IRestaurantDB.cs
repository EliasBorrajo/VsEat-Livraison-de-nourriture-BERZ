using DTO;

namespace DAL
{
    /// <summary>
    /// Interface permettant de récupérer ou modifier des informations de la table Restaurant.
    /// </summary>
    public interface IRestaurantDB
    {
        /// <summary>
        /// Méthode permettant de récupérer tous les restaurants.
        /// </summary>
        /// <returns>Tableau de Restaurant contenant tous les restaurants enregistrés dans la BD.</returns>
        Restaurant[] GetRestaurants();
        /// <summary>
        /// Méthode permettant de récupérer un restaurant par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique du restaurant.</param>
        /// <returns>Objet de type Restaurant contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Restaurant GetRestaurant(int ID);
        /// <summary>
        /// Méthode permettant de récupérer un restaurant par l'identifiant unique d'un plat.
        /// </summary>
        /// <param name="ID">Identifiant unique du plat.</param>
        /// <returns>Objet de type Restaurant contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Restaurant GetRestaurantByPlat(int ID);
    }
}
