using System;

namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Commande.
    /// </summary>
    public class Commande : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Staff en charge de livrer la commande.
        /// </summary>
        public Staff Staff { get; set; }
        /// <summary>
        /// Client qui a passé la commande.
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Plats contenus dans la commande, liés à leur quantité respective.
        /// </summary>
        public CommandePlat[] Plats { get; set; }
        /// <summary>
        /// Heure à laquelle la commande a été passée.
        /// </summary>
        public DateTime Heure { get; set; }
        /// <summary>
        /// Heure à laquelle le client a demandé la livraison.
        /// </summary>
        public DateTime HeureLivraison { get; set; }
        /// <summary>
        /// Heure à laquelle le staff a livré la commande et encaissé le paiement.
        /// </summary>
        public DateTime HeurePaiement { get; set; }
        /// <summary>
        /// Prix total de la commande.
        /// </summary>
        public double Somme { get; set; }
        /// <summary>
        /// Indique si la commande a été annulée.
        /// </summary>
        public bool Annule { get; set; }

        /// <summary>
        /// Constructeur pour créer un objet Commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <param name="Staff">Staff en charge de la commande.</param>
        /// <param name="Client">Client qui a passé la commande.</param>
        /// <param name="Plats">Plats contenus dans la commande, avec la quantité.</param>
        /// <param name="Heure">Heure à laquelle la commande a été passée.</param>
        /// <param name="HeureLivraison">Heure à laquelle la livraison à été demandée.</param>
        /// <param name="HeurePaiement">Heure à laquelle le staff a livré la commande et encaissé le paiement.</param>
        /// <param name="Somme">Prix total de la commande.</param>
        /// <param name="Annule">Indique si la commande a été annulée.</param>
        public Commande(int ID, Staff Staff, Client Client, CommandePlat[] Plats, DateTime Heure, DateTime HeureLivraison, DateTime HeurePaiement, double Somme, bool Annule)
        {
            this.ID = ID;
            this.Staff = Staff;
            this.Client = Client;
            this.Plats = Plats;
            this.Heure = Heure;
            this.HeureLivraison = HeureLivraison;
            this.HeurePaiement = HeurePaiement;
            this.Somme = Somme;
            this.Annule = Annule;
        }
    }
}
