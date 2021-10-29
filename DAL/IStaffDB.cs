using DTO;

namespace DAL
{
    public interface IStaffDB
    {
        Staff GetStaff(int ID);
        Staff GetStaff(string Mail, string Password);
        Staff AddStaff(Staff NewStaff);
        Staff[] GetStaffs();
        Staff[] GetDispStaffs(Localite Localite);
        void UpdateStaff(Staff Staff);
        void DeleteStaff(Staff Staff);
    }
}
