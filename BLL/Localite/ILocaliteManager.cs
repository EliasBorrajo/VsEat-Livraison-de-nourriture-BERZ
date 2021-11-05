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
    }
}
