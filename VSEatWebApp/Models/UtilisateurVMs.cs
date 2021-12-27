using System.ComponentModel.DataAnnotations;

namespace VSEatWebApp.Models
{
    /// <summary>
    /// Classe vue-modèle utilisée pour l'écran d'accueil du staff et des clients.
    /// </summary>
    public class SimpleUtilisateurVM
    {
        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer votre nom.")]
        public string Nom { get; set; }
        /// <summary>
        /// Prénom de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer votre prénom.")]
        public string Prenom { get; set; }
    }
    /// <summary>
    /// Classe vue-modèle utilisée pour le formulaire de base utilisateur.
    /// </summary>
    public class DetailedUtilisateurVM : SimpleUtilisateurVM
    {
        /// <summary>
        /// Téléphone de l'utilisateur.
        /// </summary>
        [DataType(DataType.PhoneNumber, ErrorMessage = "Veuillez entrer un numéro de téléphone valide.")]
        public virtual string Telephone { get; set; }
        /// <summary>
        /// Email de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer votre adresse email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Veuillez entrer une adresse email valide.")]
        public string Mail { get; set; }
        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer un mot de passe.")]
        public string Password { get; set; }
        /// <summary>
        /// Toutes les localités, utilisé comme source pour les listes à choix.
        /// </summary>
        public DTO.Localite[] AllLocalites { get; set; }
    }
}
