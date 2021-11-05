namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Client.
    /// </summary>
    public class Client : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Localité dans laquelle le client habite.
        /// </summary>
        public Localite Localite { get; set; }
        /// <summary>
        /// Nom du client.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Prénom du client.
        /// </summary>
        public string Prenom { get; set; }
        /// <summary>
        /// Téléphone du client.
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Mail du client, utilisé comme identifiant de connexion.
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Mot de passe du client.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Adresse du client.
        /// </summary>
        public string Adresse { get; set; }
        /// <summary>
        /// Statut du client, actif si true, inactif si false.
        /// </summary>
        public bool Status { get; set; }

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
        public Client(int ID, Localite Localite, string Nom, string Prenom, string Telephone, string Mail, string Password, string Adresse, bool Status)
        {
            this.ID = ID;
            this.Localite = Localite;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Telephone = Telephone;
            this.Mail = Mail;
            this.Password = Password;
            this.Adresse = Adresse;
            this.Status = Status;
        }
    }
}