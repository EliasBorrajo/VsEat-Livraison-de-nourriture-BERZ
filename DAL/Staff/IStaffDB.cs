using DTO;

namespace DAL
{
    public interface IStaffDB
    {
        Staff GetStaff(int ID);
        Staff GetStaff(string Mail, string Password);
        Staff[] GetStaffs();
        Staff[] GetDispStaffs(Localite Localite);
        Staff AddStaff(Staff NewStaff);
        void UpdateStaff(Staff Staff);
        void DeleteStaff(Staff Staff);
    }
}
