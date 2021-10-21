namespace DAL
{
    public class Client : IDBItem
    {
        public int ID { get; }
        public Localite Localite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Adresse { get; set; }

        public Client(int ID, Localite Localite, string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse)
        {
            this.ID = ID;
            this.Localite = Localite;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Telephone = Telephone;
            this.Mail = Mail;
            this.Password = Password;
            this.Adresse = Adresse;
            string test = "test";
        }
    }
}