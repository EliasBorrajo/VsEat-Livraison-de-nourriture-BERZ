using DTO;
using System;

namespace BLL
{
    public interface ICommandeManager
    {
        Commande GetCommande(int ID);
        Commande[] GetStaffCommandes(int ID, bool? EnCours);
        Commande[] GetClientCommandes(int ID, bool? EnCours);
        Commande AddCommande(Client Client, CommandePlat[] Plats, DateTime Heure, DateTime HeureLivraison, double Somme);
        void CancelCommande(Commande Commande);
    }
}
