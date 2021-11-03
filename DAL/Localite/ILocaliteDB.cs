using DTO;

namespace DAL
{
    public interface ILocaliteDB
    {
        Localite GetLocalite(int ID);
        Localite[] GetLocalites();
        Localite[] GetStaffLocalites(int ID);
        void SetStaffLocalites(int ID, Localite[] Localites);
    }
}
