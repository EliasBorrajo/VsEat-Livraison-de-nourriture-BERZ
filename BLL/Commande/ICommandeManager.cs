using DTO;
using System;

namespace BLL
{
    public interface ICommandeManager
    {
        Commande GetCommande(int ID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="EnCours">Si null, retourne toutes les commandes du staff, si true retourne toutes les commandes en cours, si false retourne toutes les commandes passées.</param>
        /// <returns></returns>
        Commande[] GetStaffCommandes(int ID, bool? EnCours);
        Commande[] GetClientCommandes(int ID, bool? EnCours);
        Commande AddCommande(Client Client, CommandePlat[] Plats, DateTime Heure, DateTime HeureLivraison);
        void CancelCommande(Commande Commande);
    }
}
