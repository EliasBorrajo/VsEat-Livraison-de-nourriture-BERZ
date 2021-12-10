using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class ClientVM : DetailedUtilisateurVM
    {
        [Required]
        public string Adresse { get; set; }
        [Required]
        public int LocaliteID { get; set; }
    }
}
