using DTO;

namespace BLL
{
    /// <summary>
    /// Interface permettant de gérer tout ce qui concerne les restaurants.
    /// </summary>
    public interface IRestaurantManager
    {
        /// <summary>
        /// Méthode permettant de récupérer tous les restaurants.
        /// </summary>
        /// <returns>Tableau de Restaurant contenant tous les restaurants.</returns>
        Restaurant[] GetRestaurants();
    }
}
