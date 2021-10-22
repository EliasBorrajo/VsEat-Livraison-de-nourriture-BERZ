using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ILocaliteDB
    {
        Localite GetLocalite(int ID);
        Localite[] GetLocalites();
        Localite[] GetStaffLocalites(int ID);
        Localite[] SetStaffLocalites(int ID, Localite[] Localites);
    }
}
