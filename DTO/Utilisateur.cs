using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public abstract class Utilisateur : IDBTable
    {
        public int ID { get; }
        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        public string Nom { get; set; }
        /// <summary>
        /// Prénom de l'utilisateur.
        /// </summary>
        public string Prenom { get; set; }
        /// <summary>
        /// Téléphone de l'utilisateur.
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Mail de l'utilisateur, utilisé comme identifiant de connexion.
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Statut de l'utilisateur, actif si true, inactif si false.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Constructeur pour créer un object utilisateur.
        /// </summary>
        /// <param name="ID">Identifiant unique de l'utilisateur.</param>
        /// <param name="Nom">Nom de l'utilisateur.</param>
        /// <param name="Prenom">Prénom de l'utilisateur.</param>
        /// <param name="Telephone">Téléphone de l'utilisateur.</param>
        /// <param name="Mail">Mail de l'utilisateur.</param>
        /// <param name="Password">Mot de passe de l'utilisateur.</param>
        /// <param name="Status">Statut de l'utilisateur.</param>
        public Utilisateur(int ID, string Nom, string Prenom, string Telephone, string Mail, string Password, bool Status)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Prenom = Prenom;
            this.Telephone = Telephone;
            this.Mail = Mail;
            this.Password = Password;
            this.Status = Status;
        }
    }
}
