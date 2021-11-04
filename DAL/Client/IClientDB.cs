using DTO;

namespace DAL
{
    public interface IClientDB
    {
        Client GetClient(int ID);
        Client GetClient(string Mail, string Password);
        Client AddClient(Client Client);
        void UpdateClient(Client Client);
    }
}
