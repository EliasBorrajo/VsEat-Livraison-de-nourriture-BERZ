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
                if (GetStaffCommandes(staff, true).Length < 5)
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
                if (commande.Client.Nom == Nom && commande.Client.Prenom == Prenom && commande.HeureLivraison.AddHours(-3) > DateTime.Now)
                {
                    commande.Annule = true;
                    CommandeDB.UpdateCommande(commande);
                }
            }
            return GetCommande(ID);
        }
    }
}
