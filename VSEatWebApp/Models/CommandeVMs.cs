using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VSEatWebApp.Models
{
    /// <summary>
    /// Classe utilisée pour la liste et le détail des commandes, pour déterminer quelle action l'utilisateur connecté peut faire.
    /// </summary>
    public class CommandeAction
    {
        /// <summary>
        /// Indique si l'utilisateur pourra réaliser l'action liée.
        /// </summary>
        public bool Display { get; set; }
        /// <summary>
        /// Action liée au type d'utilisateur.
        /// </summary>
        public string Action { get; set; }
    }
    /// <summary>
    /// Classe de base utilisée pour afficher le détail d'une commande.
    /// </summary>
    public class BaseCommandeVM
    {
        /// <summary>
        /// Commande complète.
        /// </summary>
        public DTO.Commande Commande { get; set; }
        /// <summary>
        /// Indicateur si la commande est en cours ou terminée.
        /// </summary>
        public bool EnCours { get; set; }
        /// <summary>
        /// Restaurant lié à la commande.
        /// </summary>
        public DTO.Restaurant Restaurant { get; set; }
    }
    /// <summary>
    /// Classe vue-modèle pour la liste et les détails d'une commmande.
    /// </summary>
    public class CommandeVM : BaseCommandeVM
    {
        /// <summary>
        /// Object action permettant de savoir quelle action est liée à l'utilisateur.
        /// </summary>
        public CommandeAction Action { get; set; }
    }
    /// <summary>
    /// Classe vue-modèle pour l'ajout d'une commande.
    /// </summary>
    public class AddCommandeVM
    {
        // Pour afficher les informations
        /// <summary>
        /// Identifiant unique du restaurant.
        /// </summary>
        public int RestaurantID { get; set; }
        /// <summary>
        /// Restaurant complet.
        /// </summary>
        public DTO.Restaurant Restaurant { get; set; }
        /// <summary>
        /// Source des heures de livraison possibles.
        /// </summary>
        public List<DateTime> HeuresPossibles { get; set; }
        // Pour récupérer les informations
        /// <summary>
        /// Heure de livraison sélectionnée par l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez sélectionner une heure de livraison.")]
        public DateTime HeureLivraison { get; set; }
        /// <summary>
        /// Quantités des plats choisis par l'utilisateur.
        /// </summary>
        public Dictionary<int, int> PlatsQuantites { get; set; }
    }
    /// <summary>
    /// Class vue-modèle pour l'annulation d'une commande.
    /// </summary>
    public class CancelCommandeVM : BaseCommandeVM
    {
        // Pour afficher les informations
        /// <summary>
        /// Identifiant unique de la commande.
        /// </summary>
        public int CommandeID { get; set; }
        // Pour récupérer les informations
        /// <summary>
        /// Identifiant unique de la commande, saisi par l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Le numéro de la commande doit être saisi.")]
        [Range(0, int.MaxValue, ErrorMessage = "Le numéro de la commande ne peut pas être négatif.")]
        public int ControleID { get; set; }
        /// <summary>
        /// Nom du client, saisi par l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Votre nom doit être saisi.")]
        public string Nom { get; set; }
        /// <summary>
        /// Prénom du client, saisi par l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Votre prénom doit être saisi.")]
        public string Prenom { get; set; }
    }
}
