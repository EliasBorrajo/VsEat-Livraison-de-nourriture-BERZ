using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
        public Commande[] GetStaffCommandes(int ID)
        {
            List<Commande> commandes = new List<Commande>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select comID, staID, cliID, comHeure, comHeureLivraison, comHeurePaiement, comSomme, comAnnule from Commande where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DateTime heurePaiement = DateTime.MinValue;
                            if (dr["comHeurePaiement"] != DBNull.Value) { heurePaiement = (DateTime)dr["comHeurePaiement"]; }
                            commandes.Add(new Commande(
                                (int)dr["comID"],
                                StaffDB.GetStaff((int)dr["staID"]),
                                ClientDB.GetClient((int)dr["cliID"]),
                                PlatDB.GetCommandePlats((int)dr["comID"]),
                                (DateTime)dr["comHeure"],
                                (DateTime)dr["comHeureLivraison"],
                                heurePaiement,
                                (double)dr["comSomme"],
                                (int)dr["comAnnule"] == 1));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return commandes.ToArray();
        }
        public Commande[] GetClientCommandes(int ID)
        {
            List<Commande> commandes = new List<Commande>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select comID, staID, cliID, comHeure, comHeureLivraison, comHeurePaiement, comSomme, comAnnule from Commande where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Staff staff = null;
                            if (dr["staID"] != null) { staff = StaffDB.GetStaff((int)dr["staID"]); }
                            DateTime heurePaiement = DateTime.MinValue;
                            if (dr["comHeurePaiement"] != DBNull.Value) { heurePaiement = (DateTime)dr["comHeurePaiement"]; }
                            commandes.Add(new Commande(
                                (int)dr["comID"],
                                staff,
                                ClientDB.GetClient((int)dr["cliID"]),
                                PlatDB.GetCommandePlats((int)dr["comID"]),
                                (DateTime)dr["comHeure"],
                                (DateTime)dr["comHeureLivraison"],
                                heurePaiement,
                                (double)dr["comSomme"],
                                (int)dr["comAnnule"] == 1));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return commandes.ToArray();
        }
        public Commande AddCommande(Commande NewCommande)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "insert into Commande ({0}cliID, comHeure, comHeureLivraison, {2}comSomme, comAnnule) values ({1}@cliID, @comHeure, @comHeureLivraison, {3}@comSomme, @comAnnule);select scope_identity()";
                    string staff0 = string.Empty;
                    string staff1 = string.Empty;
                    string heurePaiement2 = string.Empty;
                    string heurePaiement3 = string.Empty;
                    if (NewCommande.Staff != null)
                    {
                        staff0 = "staID, ";
                        staff1 = "@staID, ";
                    }
                    if (NewCommande.HeurePaiement > DateTime.MinValue)
                    {
                        heurePaiement2 = "comHeurePaiement, ";
                        heurePaiement3 = "@comHeurePaiement, ";
                    }
                    string.Format(query, staff0, staff1, heurePaiement2, heurePaiement3);
                    SqlCommand cmd = new SqlCommand(query, cn);
                    if (NewCommande.Staff != null) { cmd.Parameters.AddWithValue("@staID", NewCommande.Staff.ID); }
                    cmd.Parameters.AddWithValue("@cliID", NewCommande.Client.ID);
                    cmd.Parameters.AddWithValue("@comHeure", NewCommande.Heure);
                    cmd.Parameters.AddWithValue("@comHeureLivraison", NewCommande.HeureLivraison);
                    if (NewCommande.HeurePaiement > DateTime.MinValue) { cmd.Parameters.AddWithValue("@comHeurePaiement", NewCommande.HeurePaiement); }
                    cmd.Parameters.AddWithValue("@comSomme", NewCommande.Somme);
                    cmd.Parameters.AddWithValue("@comAnnule", NewCommande.Annule ? "1" : "0");
                    cn.Open();
                    int newid = Convert.ToInt32(cmd.ExecuteScalar());
                    return GetCommande(newid);
                }
            }
            catch (Exception e) { throw e; }
        }
        public void CancelCommande(Commande Commande)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "update Commande set comAnnule=1 where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Commande.ID);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
        public void DeleteCommandes(Staff Staff)
        {
            Commande[] staffCommandes = GetStaffCommandes(Staff.ID);
            foreach (Commande commande in staffCommandes)
            {
                DeleteCommande(commande);
            }
        }
        public void DeleteCommandes(Client Client)
        {
            Commande[] clientCommandes = GetClientCommandes(Client.ID);
            foreach (Commande commande in clientCommandes)
            {
                DeleteCommande(commande);
            }
        }

        private void DeleteCommande(Commande Commande)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "delete from Commande where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Commande.ID);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}
