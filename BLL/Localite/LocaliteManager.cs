using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    /// <summary>
    /// Classe qui implémente l'interface ILocaliteManager, permettant la gestion de tout ce qui concerne les localités.
    /// </summary>
    public class LocaliteManager : ILocaliteManager
    {
        /// <summary>
        /// Objet permettant d'interagir avec la table Localite.
        /// </summary>
        private ILocaliteDB LocaliteDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet LocaliteManager.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public LocaliteManager(IConfiguration Configuration)
        {
            LocaliteDB = new LocaliteDB(Configuration);
        }

        public Localite[] GetLocalites()
        {
            return LocaliteDB.GetLocalites();
        }
    }
}
