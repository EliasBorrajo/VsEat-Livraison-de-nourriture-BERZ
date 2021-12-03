using DTO;

namespace BLL
{
    /// <summary>
    /// Interface permettant de gérer tout ce qui concerne le staff.
    /// </summary>
    public interface IStaffManager
    {
        /// <summary>
        /// Méthode permettant de récupérer un staff par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique du staff.</param>
        /// <returns>Objet de type Staff contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Staff GetStaff(int ID);
        /// <summary>
        /// Méthode permettant de récupérer un staff par son adresse mail et son mot de passe.
        /// </summary>
        /// <param name="Mail">Mail du staff.</param>
        /// <param name="Password">Mot de passe du staff.</param>
        /// <returns>Objet de type Staff contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Staff GetStaff(string Mail, string Password);
        /// <summary>
        /// Méthode permettant d'ajouter un staff.
        /// </summary>
        /// <param name="Nom">Nom du staff.</param>
        /// <param name="Prenom">Prénom du staff.</param>
        /// <param name="Telephone">Téléphone du staff.</param>
        /// <param name="Mail">Mail du staff.</param>
        /// <param name="Password">Mot de passe du staff.</param>
        /// <returns>Objet de type Staff contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password);
        /// <summary>
        /// Méthode permettant d'ajouter un staff.
        /// </summary>
        /// <param name="Nom">Nom du staff.</param>
        /// <param name="Prenom">Prénom du staff.</param>
        /// <param name="Telephone">Téléphone du staff.</param>
        /// <param name="Mail">Mail du staff.</param>
        /// <param name="Password">Mot de passe du staff.</param>
        /// <param name="Localites">Localités dans lesquelles le staff travaille.</param>
        /// <returns>Objet de type Staff contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites);
        /// <summary>
        /// Méthode permettant de mettre à jour un staff.
        /// </summary>
        /// <param name="Staff">Staff contenant les informations à mettre à jour.</param>
        void UpdateStaff(Staff Staff);
        /// <summary>
        /// Méthode permettant de désactiver un staff.
        /// </summary>
        /// <param name="Staff">Staff à désactiver.</param>
        void DisableStaff(Staff Staff);
    }
}
