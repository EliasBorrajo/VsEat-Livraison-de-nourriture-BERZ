namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Restaurant.
    /// </summary>
    public class Restaurant : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Localité où le restaurant est situé.
        /// </summary>
        public Localite Localite { get; set; }
        /// <summary>
        /// Nom du restaurant.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Adresse du restaurant.
        /// </summary>
        public string Adresse { get; set; }
        /// <summary>
        /// Plats du restaurant.
        /// </summary>
        public Plat[] Plats { get; set; }
        
        /// <summary>
        /// Constructeur pour créer un objet restaurant.
        /// </summary>
        /// <param name="ID">Identifiant unique du restaurant.</param>
        /// <param name="Localite">Localité où le restaurant est situé.</param>
        /// <param name="Nom">Nom du restaurant.</param>
        /// <param name="Adresse">Adresse du restaurant.</param>
        /// <param name="Plats">Plats du restaurant.</param>
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
