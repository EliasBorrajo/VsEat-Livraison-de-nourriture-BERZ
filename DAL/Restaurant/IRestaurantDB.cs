using DTO;

namespace DAL
{
    public interface IRestaurantDB
    {
        Restaurant[] GetRestaurants();
        Restaurant GetRestaurant(int ID);
    }
}
