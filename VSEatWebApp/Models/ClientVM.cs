using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class ClientVM : DetailedUtilisateurVM
    {
        public string Adresse { get; set; }
        public LocaliteVM Localite { get; set; }
    }
}
