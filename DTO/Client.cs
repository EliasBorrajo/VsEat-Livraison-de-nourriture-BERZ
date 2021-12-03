namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Client.
    /// </summary>
    public class Client : Utilisateur
    {
        /// <summary>
        /// Localité dans laquelle le client habite.
        /// </summary>
        public Localite Localite { get; set; }
        /// <summary>
        /// Adresse du client.
        /// </summary>
        public string Adresse { get; set; }

        /// <summary>
        /// Constructeur pour créer un objet client.
        /// </summary>
        /// <param name="ID">Identifiant unique du client.</param>
        /// <param name="Localite">Localité dans laquelle le client habite.</param>
        /// <param name="Nom">Nom du client.</param>
        /// <param name="Prenom">Prénom du client.</param>
        /// <param name="Telephone">Téléphone du client.</param>
        /// <param name="Mail">Mail du client.</param>
        /// <param name="Password">Mot de passe du client.</param>
        /// <param name="Adresse">Adresse du client.</param>
        /// <param name="Status">Statut du client.</param>
        public Client(int ID, Localite Localite, string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse, bool Status) : base(ID, Nom, Prenom, Telephone, Mail, Password, Status)
        {
            this.Localite = Localite;
            this.Adresse = Adresse;
        }
    }
}