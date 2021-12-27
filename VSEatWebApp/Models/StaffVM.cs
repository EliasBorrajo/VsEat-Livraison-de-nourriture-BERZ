using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VSEatWebApp.Models
{
    /// <summary>
    /// Classe vue-modèle utilisée pour le formulaire staff.
    /// </summary>
    public class StaffVM : DetailedUtilisateurVM
    {
        /// <summary>
        /// Identifiants uniques des localités où le staff travaille.
        /// </summary>
        [Required(ErrorMessage = "Veuillez sélectionner au moins une localité.")]
        public List<int> LocaliteIDs { get; set; }
        [Required(ErrorMessage = "Veuillez entrer votre numéro de téléphone.")]
        public override string Telephone { get { return base.Telephone; } set { base.Telephone = value; } }
    }
}
