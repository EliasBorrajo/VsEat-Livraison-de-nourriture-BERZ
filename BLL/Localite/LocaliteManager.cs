using DAL;
using DTO;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="LocaliteDB">Objet permettant de communiquer avec la table Localite.</param>
        public LocaliteManager(ILocaliteDB LocaliteDB)
        {
            this.LocaliteDB = LocaliteDB;
        }

        public Localite GetLocalite(int ID)
        {
            Localite[] localites = GetLocalites();
            Localite localite = null;
            foreach (Localite loc in localites)
            {
                if (loc.ID == ID) { localite = loc; break; }
            }
            return localite;
        }

        public Localite[] GetLocalites(int[] IDs)
        {
            Localite[] allLocalites = GetLocalites();
            List<Localite> localites = new List<Localite>();
            foreach (Localite localite in allLocalites)
            {
                if (IDs.Contains(localite.ID))
                {
                    localites.Add(localite);
                }
            }
            return localites.ToArray();
        }

        public Localite[] GetLocalites()
        {
            return LocaliteDB.GetLocalites();
        }
    }
}
