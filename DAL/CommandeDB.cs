using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace DAL
{
    public class CommandeDB : ICommandeDB
    {
        private IConfiguration Configuration { get; }
        private IStaffDB StaffDB { get; }
        private IClientDB ClientDB { get; }
        private IPlatDB PlatDB { get; }

        public CommandeDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            StaffDB = new StaffDB(Configuration);
            ClientDB = new ClientDB(Configuration);
            PlatDB = new PlatDB(Configuration);
        }
        public Commande GetCommande(int ID)
        {
            Commande commande = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select comID, staID, cliID, comHeure, comHeureLivraison, comHeurePaiement, comSomme, comAnnule from Commande where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Staff staff = null;
                            if (dr["staID"] != DBNull.Value) { staff = StaffDB.GetStaff((int)dr["staID"]); }
                            DateTime heurePaiement = DateTime.MinValue;
                            if (dr["comHeurePaiement"] != DBNull.Value) { heurePaiement = (DateTime)dr["comHeurePaiement"]; }
                            commande = new Commande(
                                (int)dr["comID"],
                                staff,
                                ClientDB.GetClient((int)dr["cliID"]),
                                PlatDB.GetCommandePlats((int)dr["comID"]),
                                (DateTime)dr["comHeure"],
                                (DateTime)dr["comHeureLivraison"],
                                heurePaiement,
                                (double)dr["comSomme"],
                                (int)dr["comAnnule"] == 1);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return commande;
        }
    }
}
