namespace DTO
{
    public class Plat : IDBItem
    {
        public int ID { get; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Description { get; set; }
        //public Bitmap Image { get; set; }

        public Plat(int ID, string Nom, double Prix, string Description/*, Bitmap Image*/)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Prix = Prix;
            this.Description = Description;
            //this.Image = Image;
        }
    }
}
