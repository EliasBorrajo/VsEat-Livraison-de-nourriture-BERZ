namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Localite.
    /// </summary>
    public class Localite : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Nom de la localité.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// NPA de la localité.
        /// </summary>
        public string NPA { get; set; }

        /// <summary>
        /// Constructeur pour créer un objet Localite.
        /// </summary>
        /// <param name="ID">Identifiant unique de la localité.</param>
        /// <param name="Nom">Nom de la localité.</param>
        /// <param name="NPA">NPA de la localité.</param>
        public Localite(int ID, string Nom, string NPA)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.NPA = NPA;
        }
    }
}
