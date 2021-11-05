using System.Drawing;

namespace DTO
{
    public class Plat : IDBTable
    {
        public int ID { get; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }

        public Plat(int ID, string Nom, double Prix, string Description, Image Image)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Prix = Prix;
            this.Description = Description;
            this.Image = Image;
        }
    }

    public class CommandePlat : Plat
    {
        public int Quantite { get; set; }

        public CommandePlat(Plat Plat, int Quantite) : base(Plat.ID, Plat.Nom, Plat.Prix, Plat.Description, Plat.Image)
        {
            this.Quantite = Quantite;
        }
    }
}
