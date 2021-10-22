using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Localite : IDBItem
    {
        public int ID { get; }
        public string Nom { get; set; }
        public string NPA { get; set; }

        public Localite(int ID, string Nom, string NPA)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.NPA = NPA;
        }
    }
}
