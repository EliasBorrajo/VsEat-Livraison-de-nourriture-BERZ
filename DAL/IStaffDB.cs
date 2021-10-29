using DTO;

namespace DAL
{
    public interface IStaffDB
    {
        Staff GetStaff(int ID);
        Staff AddStaff(Staff NewStaff);
    }
}
