using DTO;

namespace DAL
{
    public interface IPlatDB
    {
        Plat GetPlat(int ID);
        Plat[] GetRestaurantPlats(int ID);
        CommandePlat[] GetCommandePlats(int ID);
    }
}
