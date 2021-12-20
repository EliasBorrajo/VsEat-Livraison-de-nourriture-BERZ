using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VSEatWebApp.Models
{
    public class SimpleUtilisateurVM
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
    }
    public class DetailedUtilisateurVM : SimpleUtilisateurVM
    {
        [DataType(DataType.PhoneNumber)]
        public virtual string Telephone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        public DTO.Localite[] AllLocalites { get; set; }
    }
}
