using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// Classe qui implémente l'interface ICommandeManager, permettant la gestion de tout ce qui concerne les commandes.
    /// </summary>
    public class CommandeManager : ICommandeManager
    {
        /// <summary>
        /// Objet permettant d'interagir avec la table Commande.
        /// </summary>
        private ICommandeDB CommandeDB { get; }
        /// <summary>
        /// Objet permettant d'interagir avec la table Staff.
        /// </summary>
        private IStaffDB StaffDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet CommandeManager.
        /// </summary>
        /// <param name="StaffDB">Objet permettant de communiquer avec la table Staff.</param>
        /// <param name="CommandeDB">Objet permettant de communiquer avec la table Commande.</param>
        public CommandeManager(ICommandeDB CommandeDB, IStaffDB StaffDB)
        {
            this.CommandeDB = CommandeDB;
            this.StaffDB = StaffDB;
        }

        public Commande GetCommande(int ID)
        {
            return CommandeDB.GetCommande(ID);
        }
        public Commande[] GetStaffCommandes(Staff Staff, bool? EnCours)
        {
            Commande[] commandes = CommandeDB.GetStaffCommandes(Staff);
            if (EnCours.HasValue)
            {
                if (EnCours.Value)
                {
                    List<Commande> cmdEnCours = new List<Commande>();
                    foreach (Commande cmd in commandes)
                    {
                        if (!cmd.Annule && cmd.HeurePaiement == DateTime.MinValue)
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
                        if (cmd.Annule || cmd.HeurePaiement > DateTime.MinValue)
                        {
                            cmdTerminees.Add(cmd);
                        }
                    }
                    commandes = cmdTerminees.ToArray();
                }
            }
            return commandes;
        }
        public Commande[] GetStaffCommandes(Staff Staff, DateTime HeureLivraison)
        {
            List<Commande> commandes = new List<Commande>();
            Commande[] staffCommandes = GetStaffCommandes(Staff, null);
            DateTime start, end;
            if (HeureLivraison.Minute < 30)
            {
                start = new DateTime(HeureLivraison.Year, HeureLivraison.Month, HeureLivraison.Day, HeureLivraison.Hour, 0, 0);
                end = new DateTime(HeureLivraison.Year, HeureLivraison.Month, HeureLivraison.Day, HeureLivraison.Hour, 29, 59);
            }
            else
            {
                start = new DateTime(HeureLivraison.Year, HeureLivraison.Month, HeureLivraison.Day, HeureLivraison.Hour, 30, 0);
                end = new DateTime(HeureLivraison.Year, HeureLivraison.Month, HeureLivraison.Day, HeureLivraison.Hour, 59, 59);
            }
            foreach (Commande commande in staffCommandes)
            {
                if (!commande.Annule && commande.HeureLivraison >= start && commande.HeureLivraison <= end)
                {
                    commandes.Add(commande);
                }
            }
            return commandes.ToArray();
        }
        public Commande[] GetClientCommandes(Client Client, bool? EnCours)
        {
            Commande[] commandes = CommandeDB.GetClientCommandes(Client);
            if (EnCours.HasValue)
            {
                if (EnCours.Value)
                {
                    List<Commande> cmdEnCours = new List<Commande>();
                    foreach (Commande cmd in commandes)
                    {
                        if (!cmd.Annule && cmd.HeurePaiement == DateTime.MinValue)
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
                        if (cmd.Annule || cmd.HeurePaiement > DateTime.MinValue)
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
                if (GetStaffCommandes(staff, HeureLivraison).Length < 5)
                {
                    commande = new Commande(-1, staff, Client, Plats, DateTime.Now, HeureLivraison, DateTime.MinValue, somme, false);
                    commande = CommandeDB.AddCommande(commande);
                }
            }
            return commande;
        }
        public Commande ValidatePayment(Commande Commande)
        {
            Commande.HeurePaiement = DateTime.Now;
            CommandeDB.UpdateCommande(Commande);
            return GetCommande(Commande.ID);
        }
        public Commande CancelCommande(int ID, string Nom, string Prenom)
        {
            Commande commande = CommandeDB.GetCommande(ID);
            if (commande != null)
            {
                if (commande.Client.Nom == Nom && commande.Client.Prenom == Prenom && CanBeCancelled(commande))
                {
                    commande.Annule = true;
                    CommandeDB.UpdateCommande(commande);
                }
            }
            return GetCommande(ID);
        }

        public bool IsEnCours(Commande Commande)
        {
            return !Commande.Annule && Commande.HeurePaiement < Commande.HeureLivraison;
        }

        public bool CanBeCancelled(Commande Commande)
        {
            return DateTime.Now < Commande.HeureLivraison.AddHours(-3);
        }
    }
}
