using System.Drawing;

namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Plat.
    /// </summary>
    public class Plat : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Nom du plat.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Prix unitaire du plat.
        /// </summary>
        public double Prix { get; set; }
        /// <summary>
        /// Description du plat.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Image du plat.
        /// </summary>
        public string ImageBase64 { get; set; }

        /// <summary>
        /// Constructeur pour créer un objet Plat.
        /// </summary>
        /// <param name="ID">Identifiant unique du plat.</param>
        /// <param name="Nom">Nom du plat.</param>
        /// <param name="Prix">Prix unitaire du plat.</param>
        /// <param name="Description">Description du plat.</param>
        /// <param name="Image">Image du plat.</param>
        public Plat(int ID, string Nom, double Prix, string Description, string ImageBase64)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Prix = Prix;
            this.Description = Description;
            this.ImageBase64 = ImageBase64;
        }
    }

    /// <summary>
    /// Classe destinée à stocker les plats d'une commande.
    /// </summary>
    public class CommandePlat : Plat
    {
        /// <summary>
        /// Quantité du plat.
        /// </summary>
        public int Quantite { get; set; }

        /// <summary>
        /// Constructeur pour créer un objet CommandePlat.
        /// </summary>
        /// <param name="Plat">Plat de la commande.</param>
        /// <param name="Quantite">Quantité du plat.</param>
        public CommandePlat(Plat Plat, int Quantite) : base(Plat.ID, Plat.Nom, Plat.Prix, Plat.Description, Plat.ImageBase64)
        {
            this.Quantite = Quantite;
        }
    }
}
