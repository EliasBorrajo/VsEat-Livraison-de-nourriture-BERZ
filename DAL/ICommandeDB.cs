using DTO;

namespace DAL
{
    public interface ICommandeDB
    {
        Commande GetCommande(int ID);
    }
}
