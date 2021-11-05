using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    /// <summary>
    /// Classe qui implémente l'interface IStaffManager, permettant la gestion de tout ce qui concerne le staff.
    /// </summary>
    public class StaffManager : IStaffManager
    {
        /// <summary>
        /// Objet permettant d'interagir avec la table Staff.
        /// </summary>
        private IStaffDB StaffDB { get; }
        /// <summary>
        /// Objet permettant d'interagir avec la table Localite.
        /// </summary>
        private ILocaliteDB LocaliteDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet StaffManager.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public StaffManager(IConfiguration Configuration)
        {
            StaffDB = new StaffDB(Configuration);
            LocaliteDB = new LocaliteDB(Configuration);
        }
        public Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password)
        {
            return AddStaff(Nom, Prenom, Telephone, Mail, Password, new Localite[] { });
        }
        public Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites)
        {
            Staff newStaff = new Staff(-1, Nom, Prenom, Telephone, Mail, Password, Localites, true);
            return StaffDB.AddStaff(newStaff);
        }
        public Staff GetStaff(string Mail, string Password)
        {
            return StaffDB.GetStaff(Mail, Password);
        }
        public void UpdateStaff(Staff Staff)
        {
            StaffDB.UpdateStaff(Staff);
        }
        public void DisableStaff(Staff Staff)
        {
            Staff.Status = false;
            UpdateStaff(Staff);
        }
    }
}
