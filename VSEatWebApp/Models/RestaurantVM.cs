using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class RestaurantVM
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string NomLocalite { get; set; }
        public DTO.Plat[] Plats { get; set; }
    }
}
