using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class SimpleUtilisateurVM
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
    public abstract class DetailedUtilisateurVM : SimpleUtilisateurVM
    {
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public LocaliteVM[] AllLocalites { get; set; }
    }
}
