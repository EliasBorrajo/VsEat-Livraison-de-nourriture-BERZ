using DTO;

namespace DAL
{
    /// <summary>
    /// Interface permettant de récupérer ou modifier des informations de la table Localite.
    /// </summary>
    public interface ILocaliteDB
    {
        /// <summary>
        /// Méthode permettant de récupérer une localité par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique de la localité.</param>
        /// <returns>Objet de type Localite contenant les informations de l'enregistrement. Retourne null si l'enregistrement n'existe pas.</returns>
        Localite GetLocalite(int ID);
        /// <summary>
        /// Méthode permettant de récupérer les localités dans lesquelles un staff travaille.
        /// </summary>
        /// <param name="Staff">Staff dont on souhaite récupérer les localités.</param>
        /// <returns>Tableau de Localite où le staff travaille.</returns>
        Localite[] GetStaffLocalites(Staff Staff);
        /// <summary>
        /// Méthode permettant de récupérer les localités.
        /// </summary>
        /// <returns>Tableau de Localite contenant toutes les localités enregistrées dans la DB.</returns>
        Localite[] GetLocalites();
        /// <summary>
        /// Méthode permettant de définir dans quelles localités un staff travaille.
        /// </summary>
        /// <param name="Staff">Staff dont on souhaite définir les localités.</param>
        /// <param name="Localites">Localités dans lesquelles le staff travaille.</param>
        void SetStaffLocalites(Staff Staff, Localite[] Localites);
    }
}
