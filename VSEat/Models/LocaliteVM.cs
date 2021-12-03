using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSEat.Models
{
    public class LocaliteVM
    {
        public string Nom { get; set; }
        public string NPA { get; set; }
        
        public LocaliteVM(string Nom, string NPA)
        {
            this.Nom = Nom;
            this.NPA = NPA;
        }
    }
}
