using System.ComponentModel.DataAnnotations;

namespace VSEatWebApp.Models
{
    /// <summary>
    /// Classe vue-modèle utilisée pour le formulaire client.
    /// </summary>
    public class ClientVM : DetailedUtilisateurVM
    {
        /// <summary>
        /// Adresse du client.
        /// </summary>
        [Required(ErrorMessage = "Veuillez entrer votre adresse.")]
        public string Adresse { get; set; }
        /// <summary>
        /// Identifiant unique de la localité du client.
        /// </summary>
        [Required(ErrorMessage = "Veuillez sélectionner une localité.")]
        public int LocaliteID { get; set; }
    }
}
