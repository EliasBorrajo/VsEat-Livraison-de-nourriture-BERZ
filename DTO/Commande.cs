using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Commande : IDBItem
    {
        public int ID { get; }
        public Staff Staff { get; set; }
        public Client Client { get; set; }
        public Plat[] Plats { get; set; }
        public DateTime Heure { get; set; }
        public DateTime HeureLivraison { get; set; }
        public DateTime HeurePaiement { get; set; }
        public double Somme { get; set; }
        public bool Annule { get; set; }

        public Commande(int ID, Staff Staff, Client Client, Plat[] Plats, DateTime Heure, DateTime HeureLivraison, DateTime HeurePaiement, double Somme, bool Annule)
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
