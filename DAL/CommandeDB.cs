using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommandeDB : ICommandeDB
    {
        private IConfiguration Configuration { get; }
        private StaffDB StaffDB { get; }
        private ClientDB ClientDB { get; }
        private PlatDB PlatDB { get; }

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
                            commande = new Commande(
                                (int)dr["comID"],
                                StaffDB.GetStaff((int)dr["staID"]),//
                                ClientDB.GetClient((int)dr["cliID"]),
                                PlatDB.GetCommandePlats((int)dr["comID"]),
                                (DateTime)dr["comHeure"],
                                (DateTime)dr["comHeureLivraison"],
                                (DateTime)dr["comHeurePaiement"],//
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
