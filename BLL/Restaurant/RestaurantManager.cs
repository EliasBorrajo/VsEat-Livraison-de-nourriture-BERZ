using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

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
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public RestaurantManager(IConfiguration Configuration)
        {
            RestaurantDB = new RestaurantDB(Configuration);
        }

        public Restaurant[] GetRestaurants()
        {
            return RestaurantDB.GetRestaurants();
        }
    }
}
