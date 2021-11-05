using DTO;

namespace DAL
{
    /// <summary>
    /// Interface permettant de récupérer ou modifier des informations de la table Staff. 
    /// </summary>
    public interface IStaffDB
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
        //Staff[] GetStaffs();
        /// <summary>
        /// Méthode permettant de récupérer les staffs qui travaillent dans une localité.
        /// </summary>
        /// <param name="Localite">Localité dans laquelle le staff doit travailler.</param>
        /// <returns>Tableau de Staff contenant les staffs qui travaillent dans la localité demandée. Retourne un tableau vide si aucun staff ne travaille dans la localité.</returns>
        Staff[] GetStaffWorkingIn(Localite Localite);
        /// <summary>
        /// Méthode permettant d'ajouter un staff.
        /// </summary>
        /// <param name="Staff">Staff à ajouter, doit contenir toutes les informations à enregistrer dans la BD.</param>
        /// <returns>Objet de type Staff contenant les informations enregistrées dans la BD, y compris l'identifiant unique.</returns>
        Staff AddStaff(Staff Staff);
        /// <summary>
        /// Méthode permettant de mettre à jour les informations d'un staff.
        /// </summary>
        /// <param name="Staff">Staff à modifier, doit contenir les informations modifiées.</param>
        void UpdateStaff(Staff Staff);
    }
}
