using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class CommandeVM
    {
        public DTO.Commande Commande { get; set; }
        public string Action { get; set; }
        public bool Status
        {
            get
            {
                bool rv = false;
                if (Commande != null)
                {
                    rv = Commande.HeurePaiement < Commande.Heure && !Commande.Annule;
                }
                return rv;
            }
        }
        public string StatusString
        {
            get
            {
                string rv = string.Empty;
                if (Commande != null)
                {
                    if (Status)
                    {
                        rv = "En cours";
                    }
                    else
                    {
                        rv = "Terminée";
                    }
                }
                return rv;
            }
        }
    }
    public class AddCommandeVM
    {
        [Required]
        public int RestaurantID { get; set; }
        public DTO.Restaurant Restaurant { get; set; }
        [Required]
        public DateTime HeureLivraison { get; set; }
        public Dictionary<int, int> PlatsQuantites { get; set; }
        public List<DateTime> HeuresPossibles { get; set; }
    }
    public class CancelCommandeVM
    {
        public DTO.Commande Commande { get; set; }
        [Required]
        public int CommandeID { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
    }
    public class SimpleCommandeVM
    {
        public DTO.Commande Commande { get; set; }
        public DTO.Restaurant Restaurant { get; set; }
        [Required]
        public bool EnCours { get; set; }
        [Required]
        public string Action { get; set; }
    }
}
