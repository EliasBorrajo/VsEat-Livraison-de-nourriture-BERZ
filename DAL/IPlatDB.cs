using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPlatDB
    {
        Plat GetPlat(int ID);
        Plat[] GetRestaurantPlats(int ID);
        Plat[] GetCommandePlats(int ID);
    }
}
