using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// Classe qui implémente l'interface IClientDB, permettant la récupération et la modification des informations de la table Client.
    /// </summary>
    public class ClientDB : IClientDB
    {
        /// <summary>
        /// Objet de configuration permettant la récupération de la chaîne de connexion à la DB.
        /// </summary>
        private IConfiguration Configuration { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Localite.
        /// </summary>
        private ILocaliteDB LocaliteDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet ClientDB.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public ClientDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
        }
        /// <summary>
        /// Méthode permettant de générer un objet de type Client depuis un SqlDataReader.
        /// </summary>
        /// <param name="dr">SqlDataReader provenant d'une requête select sur la table Client.</param>
        /// <returns>Objet de type Client contenant les informations récupérées depuis la table Client.</returns>
        private Client GetClientFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["cliID"];
            Localite localite = LocaliteDB.GetLocalite((int)dr["locID"]);// on va récupérer et enregistrer la localité correspondante à locID dans cet objet client
            string nom = (string)dr["cliNom"];
            string prenom = (string)dr["cliPrenom"];
            string telephone = string.Empty;
            if (dr["cliTelephone"] != DBNull.Value) { telephone = (string)dr["cliTelephone"]; }
            string mail = (string)dr["cliMail"];
            string password = (string)dr["cliPassword"];
            string adresse = (string)dr["cliAdresse"];
            bool status = (byte)dr["cliStatus"] == 1;
            return new Client(id, localite, nom, prenom, telephone, mail, password, adresse, status);
        }
        public Client GetClient(int ID)
        {
            Client client = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    // on énumère chaque colonne plutôt que de faire un select * parce que c'est plus performant !
                    string query = @"select cliID, locID, cliNom, cliPrenom, cliTelephone, cliMail, cliAdresse, cliPassword, cliStatus 
                                            from Client 
                                            where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = GetClientFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer le client par son identifiant unique.");
            }
            return client;
        }
        public Client GetClient(string Mail, string Password)
        {
            Client client = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select cliID, locID, cliNom, cliPrenom, cliTelephone, cliMail, cliAdresse, cliPassword, cliStatus 
                                            from Client 
                                            where cliMail=@cliMail and cliPassword=@cliPass";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cliMail", Mail);
                    cmd.Parameters.AddWithValue("@cliPass", Password);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = GetClientFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer le client par son adresse mail et son mot de passe.");
            }
            return client;
        }
        public Client AddClient(Client Client)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    // on ne récupère pas l'ID du nouveau client car il sera créé dans la BD.
                    string query = @"insert into Client (locID, cliNom, cliPrenom, cliTelephone, cliMail, cliAdresse, cliPassword, cliStatus) 
                                            values (@locID, @cliNom, @cliPrenom, @cliTelephone, @cliMail, @cliAdresse, @cliPassword, @cliStatus);
                                            select scope_identity()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@locID", Client.Localite.ID);
                    cmd.Parameters.AddWithValue("@cliNom", Client.Nom);
                    cmd.Parameters.AddWithValue("@cliPrenom", Client.Prenom);
                    cmd.Parameters.AddWithValue("@cliTelephone", Client.Telephone);
                    cmd.Parameters.AddWithValue("@cliMail", Client.Mail);
                    cmd.Parameters.AddWithValue("@cliAdresse", Client.Adresse);
                    cmd.Parameters.AddWithValue("@cliPassword", Client.Password);
                    cmd.Parameters.AddWithValue("@cliStatus", Client.Status);
                    cn.Open();
                    int newid = Convert.ToInt32(cmd.ExecuteScalar());
                    // on renvoie le client nouvellement inséré, avec l'id généré par la BD (récupéré avec le select scope_identity()).
                    return GetClient(newid);
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible d'ajouter le client.");
            }
        }
        public void UpdateClient(Client Client)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"update Client set locID=@loc, cliNom=@nom, cliPrenom=@pre, cliTelephone=@tel, cliMail=@mai, cliAdresse=@add, cliPassword=@pas, cliStatus=@sta 
                                            where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Client.ID);
                    cmd.Parameters.AddWithValue("@loc", Client.Localite.ID);
                    cmd.Parameters.AddWithValue("@nom", Client.Nom);
                    cmd.Parameters.AddWithValue("@pre", Client.Prenom);
                    cmd.Parameters.AddWithValue("@tel", Client.Telephone);
                    cmd.Parameters.AddWithValue("@mai", Client.Mail);
                    cmd.Parameters.AddWithValue("@add", Client.Adresse);
                    cmd.Parameters.AddWithValue("@pas", Client.Password);
                    cmd.Parameters.AddWithValue("@sta", Client.Status);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de mettre à jour le client.");
            }
        }
    }
}
