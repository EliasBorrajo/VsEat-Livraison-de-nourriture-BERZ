namespace DTO
{
    /// <summary>
    /// Classe destinée à stocker les enregistrements de la table Staff.
    /// </summary>
    public class Staff : Utilisateur
    {
        /// <summary>
        /// Localités dans lesquelles le staff travaille.
        /// </summary>
        public Localite[] Localites { get; set; }

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
        public Staff(int ID, string Nom, string Prenom, string Telephone, string Mail, string Password, Localite[] Localites, bool Status) : base(ID, Nom, Prenom, Telephone, Mail, Password, Status)
        {
            this.Localites = Localites;
        }
    }
}
