using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class ClientDB : IClientDB
    {
        private IConfiguration Configuration { get; }
        private LocaliteDB LocaliteDB { get; }

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
    }
}
