using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class StaffVM : DetailedUtilisateurVM
    {
        public LocaliteVM[] Localites { get; set; }
    }
}
