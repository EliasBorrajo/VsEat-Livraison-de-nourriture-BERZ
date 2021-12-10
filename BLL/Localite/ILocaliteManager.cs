using DTO;

namespace BLL
{
    /// <summary>
    /// Interface permettant de gérer tout ce qui concerne les localités.
    /// </summary>
    public interface ILocaliteManager
    {
        /// <summary>
        /// Méthode permettant de récupérer toutes les localités.
        /// </summary>
        /// <returns>Tableau de Localite contenant toutes les localités.</returns>
        Localite[] GetLocalites();
        /// <summary>
        /// Méthode permettant de récupérer des localités.
        /// </summary>
        /// <param name="IDs">Identifiants des localités.</param>
        /// <returns>Tableau de Localite contenant les localités demandées.</returns>
        Localite[] GetLocalites(int[] IDs);
        /// <summary>
        /// Méthode permettant de récupérer une localité par son identifiant unique.
        /// </summary>
        /// <param name="ID">Identifiant unique de la localité.</param>
        /// <returns>Localité correspondant à l'identifiant unique, retourne null si l'enregistrement n'existe pas.</returns>
        Localite GetLocalite(int ID);
    }
}
