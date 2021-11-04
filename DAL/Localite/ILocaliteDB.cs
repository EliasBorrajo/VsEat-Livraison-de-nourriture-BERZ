using DTO;

namespace DAL
{
    public interface ILocaliteDB
    {
        Localite GetLocalite(int ID);
        Localite[] GetStaffLocalites(int ID);
        Localite[] GetLocalites();
        void SetStaffLocalites(int ID, Localite[] Localites);
    }
}
