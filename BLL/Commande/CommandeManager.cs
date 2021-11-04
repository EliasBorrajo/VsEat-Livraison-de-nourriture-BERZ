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
                        if (cmd.HeurePaiement == DateTime.MinValue)
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
                        if (cmd.HeurePaiement > DateTime.MinValue)
                        {
                            cmdTerminees.Add(cmd);
                        }
                    }
                    commandes = cmdTerminees.ToArray();
                }
            }
            return commandes;
        }
        public Commande AddCommande(Client Client, Restaurant Restaurant, CommandePlat[] Plats, DateTime HeureLivraison)
        {
            Commande commande = null;
            double somme = 0;
            foreach (CommandePlat commandePlat in Plats)
            {
                somme += commandePlat.Prix * commandePlat.Quantite;
            }
            Staff[] dispStaffs = StaffDB.GetStaffWorkingIn(Restaurant.Localite);
            foreach (Staff staff in dispStaffs)
            {
                if (GetStaffCommandes(staff.ID, true).Length < 5)
                {
                    commande = new Commande(-1, staff, Client, Plats, DateTime.Now, HeureLivraison, DateTime.MinValue, somme, false);
                    commande = CommandeDB.AddCommande(commande);
                }
            }
            return commande;
        }
        public void CancelCommande(int ID, string Nom, string Prenom)
        {
            Commande commande = CommandeDB.GetCommande(ID);
            if (commande != null)
            {
                if (commande.Client.Nom == Nom && commande.Client.Prenom == Prenom && commande.HeureLivraison.AddHours(-3) > DateTime.Now)
                {
                    commande.Annule = true;
                    CommandeDB.UpdateCommande(commande);
                }
            }
        }
    }
}
