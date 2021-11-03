using DTO;

namespace DAL
{
    public interface ICommandeDB
    {
        Commande GetCommande(int ID);
        Commande[] GetStaffCommandes(int ID);
        Commande[] GetClientCommandes(int ID);
        Commande AddCommande(Commande NewCommande);
        void CancelCommande(Commande Commande);
        void DeleteCommandes(Staff Staff);
        void DeleteCommandes(Client Client);
    }
}
