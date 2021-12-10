using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class StaffVM : DetailedUtilisateurVM
    {
        [Required]
        public List<int> LocaliteIDs { get; set; }
        [Required]
        public override string Telephone { get { return base.Telephone; } set { base.Telephone = value; } }
    }
}
