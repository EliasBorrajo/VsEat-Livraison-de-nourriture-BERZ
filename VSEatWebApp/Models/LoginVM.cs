using System.ComponentModel.DataAnnotations;

namespace VSEatWebApp.Models
{
    /// <summary>
    /// Classe vue-modèle utilisée pour la connexion client et staff.
    /// </summary>
    public class LoginVM
    {
        /// <summary>
        /// Email de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer votre adresse email.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Veuillez entrer une adresse email valide.")]
        public string Mail { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer votre mot de passe.")]
        [DataType(DataType.Password, ErrorMessage = "")]
        public string Password { get; set; }
    }
}
