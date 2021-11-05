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
        private Commande GetCommandeFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["comID"];
            Staff staff = null;
            if (dr["staID"] != DBNull.Value) { staff = StaffDB.GetStaff((int)dr["staID"]); }
            Client client = ClientDB.GetClient((int)dr["cliID"]);
            CommandePlat[] cp = PlatDB.GetCommandePlats(id);
            DateTime heure = (DateTime)dr["comHeure"];
            DateTime heureLivraison = (DateTime)dr["comHeureLivraison"];
            DateTime heurePaiement = DateTime.MinValue;
            if (dr["comHeurePaiement"] != DBNull.Value) { heurePaiement = (DateTime)dr["comHeurePaiement"]; }
            double somme = (double)dr["comSomme"];
            bool annule = (byte)dr["comAnnule"] == 1;
            return new Commande(id, staff, client, cp, heure, heureLivraison, heurePaiement, somme, annule);
        }
        public Commande GetCommande(int ID)
        {
            Commande commande = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select comID, staID, cliID, comHeure, comHeureLivraison, comHeurePaiement, comSomme, comAnnule 
                                            from Commande 
                                            where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            commande = GetCommandeFromDataReader(dr);
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
                    string query = @"select comID, staID, cliID, comHeure, comHeureLivraison, comHeurePaiement, comSomme, comAnnule 
                                            from Commande 
                                            where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            commandes.Add(GetCommandeFromDataReader(dr));
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
                    string query = @"select comID, staID, cliID, comHeure, comHeureLivraison, comHeurePaiement, comSomme, comAnnule 
                                            from Commande 
                                            where cliID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            commandes.Add(GetCommandeFromDataReader(dr));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return commandes.ToArray();
        }
        public Commande AddCommande(Commande Commande)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"insert into Commande ({0}cliID, comHeure, comHeureLivraison, {2}comSomme, comAnnule) 
                                            values ({1}@cliID, @comHeure, @comHeureLivraison, {3}@comSomme, @comAnnule);
                                            select scope_identity()";
                    string staff0 = string.Empty;
                    string staff1 = string.Empty;
                    string heurePaiement2 = string.Empty;
                    string heurePaiement3 = string.Empty;
                    if (Commande.Staff != null)
                    {
                        staff0 = "staID, ";
                        staff1 = "@staID, ";
                    }
                    if (Commande.HeurePaiement > DateTime.MinValue)
                    {
                        heurePaiement2 = "comHeurePaiement, ";
                        heurePaiement3 = "@comHeurePaiement, ";
                    }
                    query = string.Format(query, staff0, staff1, heurePaiement2, heurePaiement3);
                    SqlCommand cmd = new SqlCommand(query, cn);
                    if (Commande.Staff != null) { cmd.Parameters.AddWithValue("@staID", Commande.Staff.ID); }
                    cmd.Parameters.AddWithValue("@cliID", Commande.Client.ID);
                    cmd.Parameters.AddWithValue("@comHeure", Commande.Heure);
                    cmd.Parameters.AddWithValue("@comHeureLivraison", Commande.HeureLivraison);
                    if (Commande.HeurePaiement > DateTime.MinValue) { cmd.Parameters.AddWithValue("@comHeurePaiement", Commande.HeurePaiement); }
                    cmd.Parameters.AddWithValue("@comSomme", Commande.Somme);
                    cmd.Parameters.AddWithValue("@comAnnule", Commande.Annule ? "1" : "0");
                    cn.Open();
                    int newid = Convert.ToInt32(cmd.ExecuteScalar());
                    PlatDB.SetCommandePlats(newid, Commande.Plats);
                    return GetCommande(newid);
                }
            }
            catch (Exception e) { throw e; }
        }
        public void UpdateCommande(Commande Commande)
        {
            PlatDB.SetCommandePlats(Commande.ID, Commande.Plats);
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"update Commande set staID=@sta, cliID=@cli, comHeure=@h, comHeureLivraison=@hl, comHeurePaiement=@hp, comSomme=@som, comAnnule=@ca 
                                            where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Commande.ID);
                    cmd.Parameters.AddWithValue("@sta", Commande.Staff.ID);
                    cmd.Parameters.AddWithValue("@cli", Commande.Client.ID);
                    cmd.Parameters.AddWithValue("@h", Commande.Heure);
                    cmd.Parameters.AddWithValue("@hl", Commande.HeureLivraison);
                    cmd.Parameters.AddWithValue("@hp", Commande.HeurePaiement);
                    cmd.Parameters.AddWithValue("@som", Commande.Somme);
                    cmd.Parameters.AddWithValue("@ca", Commande.Annule ? "1" : "0");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}
