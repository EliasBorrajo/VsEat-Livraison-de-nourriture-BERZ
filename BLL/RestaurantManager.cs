using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RestaurantManager : IRestaurantManager
    {
        private IRestaurantDB RestaurantDB { get; }

        public RestaurantManager(IConfiguration Configuration)
        {
            RestaurantDB = new RestaurantDB(Configuration);
        }
    }
}
