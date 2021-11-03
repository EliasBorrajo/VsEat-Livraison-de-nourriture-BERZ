using DTO;

namespace DAL
{
    public interface IRestaurantDB
    {
        Restaurant GetRestaurant(int ID);
        Restaurant[] GetRestaurants();
    }
}
