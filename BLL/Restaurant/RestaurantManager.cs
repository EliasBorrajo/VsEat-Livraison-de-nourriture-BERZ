using DAL;
using DTO;

namespace BLL
{
    /// <summary>
    /// Classe qui implémente l'interface IRestaurantManager, permettant la gestion de tout ce qui concerne les restaurants.
    /// </summary>
    public class RestaurantManager : IRestaurantManager
    {
        /// <summary>
        /// Objet permettant d'interagir avec la table Restaurant.
        /// </summary>
        private IRestaurantDB RestaurantDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet RestaurantManager.
        /// </summary>
        /// <param name="RestaurantDB">Objet permettant de communiquer avec la table Restaurant.</param>
        public RestaurantManager(IRestaurantDB RestaurantDB)
        {
            this.RestaurantDB = RestaurantDB;
        }

        public Restaurant GetRestaurant(int ID)
        {
            Restaurant[] restaurants = GetRestaurants();
            Restaurant rv = null;
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.ID == ID)
                {
                    rv = restaurant;
                    break;
                }
            }
            return rv;
        }

        public Restaurant[] GetRestaurants()
        {
            return RestaurantDB.GetRestaurants();
        }
    }
}
