using DTO;

namespace DAL
{
    public interface IClientDB
    {
        Client GetClient(int ID);
        Client GetClient(string Mail, string Password);
        Client AddClient(Client NewClient);
        void UpdateClient(Client Client);
        void DeleteClient(Client Client);
    }
}
