using DTO;

namespace BLL
{
    /// <summary>
    /// Interface permettant de gérer tout ce qui concerne les restaurants.
    /// </summary>
    public interface IRestaurantManager
    {
        /// <summary>
        /// Méthode permettant de récupérer un restaurant par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique du restaurant.</param>
        /// <returns>Enregistrement correspondant, retourne null si l'enregistrement n'existe pas.</returns>
        Restaurant GetRestaurant(int ID);
        /// <summary>
        /// Méthode permettant de récupérer tous les restaurants.
        /// </summary>
        /// <returns>Tableau de Restaurant contenant tous les restaurants.</returns>
        Restaurant[] GetRestaurants();
    }
}
