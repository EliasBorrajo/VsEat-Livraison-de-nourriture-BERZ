using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Staff : IDBItem
    {
        public int ID { get; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public Localite[] Localites { get; set; }

        public Staff(int ID, string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Telephone = Telephone;
            this.Mail = Mail;
            this.Password = Password;
            this.Localites = Localites;
        }
    }
}
