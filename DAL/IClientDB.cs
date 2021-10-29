using DTO;

namespace DAL
{
    public interface IClientDB
    {
        Client GetClient(int ID);
        Client AddClient(Client NewClient);
    }
}
