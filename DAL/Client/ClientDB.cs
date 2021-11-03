using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class ClientDB : IClientDB
    {
        private IConfiguration Configuration { get; }
        private ILocaliteDB LocaliteDB { get; }

        public ClientDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
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
                    string query = "select cliID, locID, cliNom, cliPrenom, cliTelephone, cliMail, cliAdresse, cliPassword from Client where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string telephone = string.Empty;
                            if (dr["cliTelephone"] != DBNull.Value) { telephone = (string)dr["cliTelephone"]; }
                            client = new Client(
                                (int)dr["cliID"],
                                LocaliteDB.GetLocalite((int)dr["locID"]),// on va récupérer et enregistrer la localité correspondante à locID dans cet objet client
                                (string)dr["cliNom"],
                                (string)dr["cliPrenom"],
                                telephone,
                                (string)dr["cliMail"],
                                (string)dr["cliPassword"],
                                (string)dr["cliAdresse"]);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return client;
        }
        public Client AddClient(Client NewClient)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    // on ne récupère pas l'ID du nouveau client car il sera créé dans la BD.
                    string query = "insert into Client (locID, cliNom, cliPrenom, cliTelephone, cliMail, cliAdresse, cliPassword) values (@locID, @cliNom, @cliPrenom, @cliTelephone, @cliMail, @cliAdresse, @cliPassword);select scope_identity()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@locID", NewClient.Localite.ID);
                    cmd.Parameters.AddWithValue("@cliNom", NewClient.Nom);
                    cmd.Parameters.AddWithValue("@cliPrenom", NewClient.Prenom);
                    cmd.Parameters.AddWithValue("@cliTelephone", NewClient.Telephone);
                    cmd.Parameters.AddWithValue("@cliMail", NewClient.Mail);
                    cmd.Parameters.AddWithValue("@cliAdresse", NewClient.Adresse);
                    cmd.Parameters.AddWithValue("@cliPassword", NewClient.Password);
                    cn.Open();
                    int newid = Convert.ToInt32(cmd.ExecuteScalar());
                    // on renvoie le client nouvellement inséré, avec l'id généré par la BD (récupéré avec le select scope_identity()).
                    return GetClient(newid);
                }
            }
            catch (Exception e) { throw e; }
        }
        public Client GetClient(string Mail, string Password)
        {
            Client client = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select cliID, locID, cliNom, cliPrenom, cliTelephone, cliMail, cliAdresse, cliPassword from Client where cliMail=@cliMail and cliPassword=@cliPass";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@cliMail", Mail);
                    cmd.Parameters.AddWithValue("@cliPass", Password);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string telephone = string.Empty;
                            if (dr["cliTelephone"] != DBNull.Value) { telephone = (string)dr["cliTelephone"]; }
                            client = new Client(
                                (int)dr["cliID"],
                                LocaliteDB.GetLocalite((int)dr["locID"]),
                                (string)dr["cliNom"],
                                (string)dr["cliPrenom"],
                                telephone,
                                (string)dr["cliMail"],
                                (string)dr["cliPassword"],
                                (string)dr["cliAdresse"]);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return client;
        }
        public void UpdateClient(Client Client)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "update Client set locID=@loc, cliNom=@nom, cliPrenom=@pre, cliTelephone=@tel, cliMail=@mai, cliAdresse=@add, cliPassword=@pas where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Client.ID);
                    cmd.Parameters.AddWithValue("@loc", Client.Localite.ID);
                    cmd.Parameters.AddWithValue("@nom", Client.Nom);
                    cmd.Parameters.AddWithValue("@pre", Client.Prenom);
                    cmd.Parameters.AddWithValue("@tel", Client.Telephone);
                    cmd.Parameters.AddWithValue("@mai", Client.Mail);
                    cmd.Parameters.AddWithValue("@add", Client.Adresse);
                    cmd.Parameters.AddWithValue("@pas", Client.Password);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
        public void DeleteClient(Client Client)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "delete from Client where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Client.ID);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}
