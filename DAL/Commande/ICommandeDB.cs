using DTO;

namespace DAL
{
    public interface ICommandeDB
    {
        Commande GetCommande(int ID);
        Commande[] GetStaffCommandes(int ID);
        Commande[] GetClientCommandes(int ID);
        Commande AddCommande(Commande Commande);
        void UpdateCommande(Commande Commande);
    }
}
