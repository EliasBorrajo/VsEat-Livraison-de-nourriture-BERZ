namespace DTO
{
    public class CommandePlat : IDBItem
    {
        public int ID { get; }
        public Plat Plat { get; set; }
        public int Quantite { get; set; }

        public CommandePlat(int ID, Plat Plat, int Quantite)
        {
            this.ID = ID;
            this.Plat = Plat;
            this.Quantite = Quantite;
        }
    }
}
