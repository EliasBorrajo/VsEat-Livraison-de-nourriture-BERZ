using DTO;

namespace BLL
{
    interface IStaffManager
    {
        Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password);
        Staff AddStaff(string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites);
    }
}
