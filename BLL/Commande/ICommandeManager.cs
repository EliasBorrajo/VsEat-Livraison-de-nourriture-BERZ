using DTO;
using System;

namespace BLL
{
    /// <summary>
    /// Interface permettant de gérer tout ce qui concerne les commandes.
    /// </summary>
    public interface ICommandeManager
    {
        /// <summary>
        /// Méthode permettant de récupérer une commande par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <returns>Objet de type Commande contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Commande GetCommande(int ID);
        /// <summary>
        /// Méthode permettant de récupérer les commandes dont un staff est responsable.
        /// </summary>
        /// <param name="Staff">Staff dont on souhaite récupérer les commandes.</param>
        /// <param name="EnCours">Si null, retourne toutes les commandes du staff, si true retourne toutes les commandes en cours, si false retourne toutes les commandes passées.</param>
        /// <returns>Tableau de Commande contenant les commandes dont le staff est responsable.</returns>
        Commande[] GetStaffCommandes(Staff Staff, bool? EnCours);
        /// <summary>
        /// Méthode permettant de récupérer les commandes dont un staff est responsable dans la même demi-heure que l'heure de livraison indiquée.
        /// </summary>
        /// <param name="Staff">Staff dont on souhaite récupérer les commandes.</param>
        /// <param name="HeureLivraison">Heure de livraison indiquant la demi-heure des commandes à récupérer.</param>
        /// <returns>Toutes les commmandes (en cours et achevées) de la demi-heure indiquée concernant le staff indiqué.</returns>
        Commande[] GetStaffCommandes(Staff Staff, DateTime HeureLivraison);
        /// <summary>
        /// Méthode permettant de récupérer les commandes passées par un client.
        /// </summary>
        /// <param name="Client">Client dont on souhaite récupérer les commandes.</param>
        /// <param name="EnCours">Si null, retourne toutes les commandes passées par le client, si true retourne toutes les commandes en cours, si false retourne toutes les commandes passées.</param>
        /// <returns></returns>
        Commande[] GetClientCommandes(Client Client, bool? EnCours);
        /// <summary>
        /// Méthode permettant d'ajouter une commande.
        /// </summary>
        /// <param name="Client">Client qui passe la commande.</param>
        /// <param name="Restaurant">Restaurant dans lequel sont préparés les plats de la commande.</param>
        /// <param name="Plats">Plats de la commande, avec la quantité.</param>
        /// <param name="HeureLivraison">Heure de livraison souhaitées.</param>
        /// <returns>Commande nouvellement créée avec l'identifiant unique généré par la DB.</returns>
        Commande AddCommande(Client Client, Restaurant Restaurant, CommandePlat[] Plats, DateTime HeureLivraison);
        /// <summary>
        /// Méthode permettant de valider le paiement d'une commande.
        /// </summary>
        /// <param name="Commande">Commande à valider.</param>
        Commande ValidatePayment(Commande Commande);
        /// <summary>
        /// Méthode permettant d'annuler une commande.
        /// </summary>
        /// <param name="ID">Identifiant unique de la commande.</param>
        /// <param name="Nom">Nom du client qui a passé la commande.</param>
        /// <param name="Prenom">Prénom du client qui a passé la commande.</param>
        Commande CancelCommande(int ID, string Nom, string Prenom);
        /// <summary>
        /// Méthode indiquant si la commande est en cours, soit si elle n'a pas été annulée ou que l'heure de paiement n'a pas été saisie.
        /// </summary>
        /// <param name="Commande">Commande dont on souhaite récupérer l'information.</param>
        /// <returns>True si la commande est en cours, false autrement.</returns>
        bool IsEnCours(Commande Commande);
        /// <summary>
        /// Méthode indiquant si la commande peut être annulée.
        /// </summary>
        /// <param name="Commande">Commande dont on souhaite récupérer l'information.</param>
        /// <returns>True si la commande peut être annulée, false autrement.</returns>
        bool CanBeCancelled(Commande Commande);
    }
}
