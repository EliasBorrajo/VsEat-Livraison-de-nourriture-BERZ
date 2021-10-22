using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class StaffManager
    {
        private IStaffDB StaffDB { get; }
        private ILocaliteDB LocaliteDB { get; }

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
            Staff newStaff = new Staff(-1, Nom, Prenom, Telephone, Mail, Password, Localites);
            newStaff = StaffDB.AddStaff(newStaff);
            if (newStaff.Localites.Length == 0 && Localites.Length > 0)
            {
                LocaliteDB.SetStaffLocalites(newStaff.ID, Localites);
                newStaff = StaffDB.GetStaff(newStaff.ID);
            }
            return newStaff;
        }
    }
}
