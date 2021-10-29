using DTO;

namespace BLL
{
    public interface IStaffManager
    {
        Staff GetStaff(string Mail, string Password);
        Staff[] GetDispStaffs(Localite ClientLoc);
        Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password);
        Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites);
        void UpdateStaff(Staff Staff);
        void DeleteStaff(Staff Staff);
    }
}
