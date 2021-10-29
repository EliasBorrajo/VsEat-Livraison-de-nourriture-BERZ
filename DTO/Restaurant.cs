namespace DTO
{
    public class Restaurant : IDBItem
    {
        public int ID { get; }
        public Localite Localite { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public Plat[] Plats { get; set; }
        
        public Restaurant(int ID, Localite Localite, string Nom, string Adresse, Plat[] Plats)
        {
            this.ID = ID;
            this.Localite = Localite;
            this.Nom = Nom;
            this.Adresse = Adresse;
            this.Plats = Plats;
        }
    }
}
