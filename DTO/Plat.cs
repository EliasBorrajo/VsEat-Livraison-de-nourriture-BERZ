using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Plat : IDBItem
    {
        public int ID { get; }
        public Restaurant Restaurant { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Description { get; set; }
        public int? Quantite { get; set; }
        //public Bitmap Image { get; set; }

        public Plat(int ID, Restaurant Restaurant, string Nom, double Prix, string Description/*, Bitmap Image*/) : this(ID, Restaurant, Nom, Prix, Description, null/*, Image*/) { }
        public Plat(int ID, Restaurant Restaurant, string Nom, double Prix, string Description, int? Quantite/*, Bitmap Image*/)
        {
            this.ID = ID;
            this.Restaurant = Restaurant;
            this.Nom = Nom;
            this.Prix = Prix;
            this.Description = Description;
            this.Quantite = Quantite;
            //this.Image = Image;
        }
    }
}
