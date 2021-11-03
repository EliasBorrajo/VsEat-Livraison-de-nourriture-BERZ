using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CommandeManager : ICommandeManager
    {
        private ICommandeDB CommandeDB { get; }
        private IStaffDB StaffDB { get; }

        public CommandeManager(IConfiguration Configuration)
        {
            CommandeDB = new CommandeDB(Configuration);
            StaffDB = new StaffDB(Configuration);
        }

        public Commande GetCommande(int ID)
        {
            return CommandeDB.GetCommande(ID);
        }
        public Commande[] GetStaffCommandes(int ID, bool? EnCours)
        {
            Commande[] commandes = CommandeDB.GetStaffCommandes(ID);
            if (EnCours.HasValue)
            {
                if (EnCours.Value)
                {
                    List<Commande> cmdEnCours = new List<Commande>();
                    foreach (Commande cmd in commandes)
                    {
                        if (cmd.HeurePaiement > DateTime.MinValue)
                        {
                            cmdEnCours.Add(cmd);
                        }
                    }
                    commandes = cmdEnCours.ToArray();
                }
                else
                {
                    List<Commande> cmdTerminees = new List<Commande>();
                    foreach (Commande cmd in commandes)
                    {
                        if (cmd.HeurePaiement == DateTime.MinValue)
                        {
                            cmdTerminees.Add(cmd);
                        }
                    }
                    commandes = cmdTerminees.ToArray();
                }
            }
            return commandes;
        }
        public Commande[] GetClientCommandes(int ID, bool? EnCours)
        {
            Commande[] commandes = CommandeDB.GetClientCommandes(ID);
            if (EnCours.HasValue)
            {
                if (EnCours.Value)
                {
                    List<Commande> cmdEnCours = new List<Commande>();
                    foreach (Commande cmd in commandes)
                    {
                        if (cmd.HeurePaiement > DateTime.MinValue)
                        {
                            cmdEnCours.Add(cmd);
                        }
                    }
                    commandes = cmdEnCours.ToArray();
                }
                else
                {
                    List<Commande> cmdTerminees = new List<Commande>();
                    foreach (Commande cmd in commandes)
                    {
                        if (cmd.HeurePaiement == DateTime.MinValue)
                        {
                            cmdTerminees.Add(cmd);
                        }
                    }
                    commandes = cmdTerminees.ToArray();
                }
            }
            return commandes;
        }
        public Commande AddCommande(Client Client, CommandePlat[] Plats, DateTime Heure, DateTime HeureLivraison)
        {
            Commande commande = null;
            double somme = 0;
            foreach (CommandePlat commandePlat in Plats)
            {
                somme += commandePlat.Plat.Prix * commandePlat.Quantite;
            }
            Staff[] dispStaffs = StaffDB.GetDispStaffs(Client.Localite);
            foreach (Staff staff in dispStaffs)
            {
                if (GetStaffCommandes(staff.ID, true).Length < 5)
                {
                    commande = new Commande(-1, staff, Client, Plats, Heure, HeureLivraison, DateTime.MinValue, somme, false);
                    return CommandeDB.AddCommande(commande);
                }
            }
            return commande;
        }
        public void CancelCommande(Commande Commande)
        {
            CommandeDB.CancelCommande(Commande);
        }
    }
}
