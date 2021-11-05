namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Staff.
    /// </summary>
    public class Staff : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Nom du staff.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Prénom du staff.
        /// </summary>
        public string Prenom { get; set; }
        /// <summary>
        /// Téléphone du staff.
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Mail du staff, utilisé comme identifiant de connexion.
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Mot de passe du staff.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Localités dans lesquelles le staff travaille.
        /// </summary>
        public Localite[] Localites { get; set; }
        /// <summary>
        /// Statut du staff, actif si true, inactif si false.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Constructeur pour créer un object staff.
        /// </summary>
        /// <param name="ID">Identifiant unique du staff.</param>
        /// <param name="Nom">Nom du staff.</param>
        /// <param name="Prenom">Prénom du staff.</param>
        /// <param name="Telephone">Téléphone du staff.</param>
        /// <param name="Mail">Mail du staff.</param>
        /// <param name="Password">Mot de passe du staff.</param>
        /// <param name="Localites">Localités dans lesquelles le staff travaille.</param>
        /// <param name="Status">Statut du staff, actif si true, inactif si false.</param>
        public Staff(int ID, string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites, bool Status)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Telephone = Telephone;
            this.Mail = Mail;
            this.Password = Password;
            this.Localites = Localites;
            this.Status = Status;
        }
    }
}
