using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSEat.Models
{
    public class ClientDetailVM
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public LocaliteVM Localite { get; set; }
        public LocaliteVM[] Localites { get; }

        public ClientDetailVM(string Nom, string Prenom, string Adresse, LocaliteVM Localite, LocaliteVM[] Localites)
        {
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Adresse = Adresse;
            this.Localite = Localite;
            this.Localites = Localites;
        }
    }
}
