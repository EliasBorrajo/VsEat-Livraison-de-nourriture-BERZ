using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class StaffManager : IStaffManager
    {
        private IStaffDB StaffDB { get; }
        private ILocaliteDB LocaliteDB { get; }
        private ICommandeDB CommandeDB { get; }

        public StaffManager(IConfiguration Configuration)
        {
            StaffDB = new StaffDB(Configuration);
            LocaliteDB = new LocaliteDB(Configuration);
            CommandeDB = new CommandeDB(Configuration);
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
        public Staff GetStaff(string Mail, string Password)
        {
            return StaffDB.GetStaff(Mail, Password);
        }
        public Staff[] GetDispStaffs(Localite ClientLoc)
        {
            Staff[] staffs = StaffDB.GetStaffs();
            List<Staff> dispStaffs = new List<Staff>();
            foreach (Staff staff in staffs)
            {
                foreach (Localite loc in staff.Localites)
                {
                    if (loc.ID == ClientLoc.ID)
                    {
                        dispStaffs.Add(staff);
                        break;
                    }
                }
            }
            return dispStaffs.ToArray();
        }
        public void UpdateStaff(Staff Staff)
        {
            StaffDB.UpdateStaff(Staff);
        }
        public void DeleteStaff(Staff Staff)
        {
            CommandeDB.DeleteCommandes(Staff);
            StaffDB.DeleteStaff(Staff);
        }
    }
}
