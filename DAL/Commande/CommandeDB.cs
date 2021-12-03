using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// Classe qui implémente l'interface ICommandeDB, permettant la récupération et la modification des informations de la table Commande.
    /// </summary>
    public class CommandeDB : ICommandeDB
    {
        /// <summary>
        /// Objet de configuration permettant la récupération de la chaîne de connexion à la DB.
        /// </summary>
        private IConfiguration Configuration { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Staff.
        /// </summary>
        private IStaffDB StaffDB { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Client.
        /// </summary>
        private IClientDB ClientDB { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Plat.
        /// </summary>
        private IPlatDB PlatDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet CommandeDB.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public CommandeDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            StaffDB = new StaffDB(Configuration);
            ClientDB = new ClientDB(Configuration);
            PlatDB = new PlatDB(Configuration);
        }
        /// <summary>
        /// Méthode permettant de générer un objet de type Commande depuis un SqlDataReader.
        /// </summary>
        /// <param name="dr">SqlDataReader provenant d'une requête select sur la table Commande.</param>
        /// <returns>Objet de type Commande contenant les informations récupérées depuis la table Commande.</returns>
        private Commande GetCommandeFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["comID"];
            Staff staff = null;
            if (dr["staID"] != DBNull.Value) { staff = StaffDB.GetStaff((int)dr["staID"]); }
            Client client = ClientDB.GetClient((int)dr["cliID"]);
            CommandePlat[] cp = new CommandePlat[] { };
            DateTime heure = (DateTime)dr["comHeure"];
            DateTime heureLivraison = (DateTime)dr["comHeureLivraison"];
            DateTime heurePaiement = DateTime.MinValue;
            if (dr["comHeurePaiement"] != DBNull.Value) { heurePaiement = (DateTime)dr["comHeurePaiement"]; }
            double somme = (double)dr["comSomme"];
            bool annule = (byte)dr["comAnnule"] == 1;
            Commande commande = new Commande(id, staff, client, cp, heure, heureLivraison, heurePaiement, somme, annule);
            commande.Plats = PlatDB.GetCommandePlats(commande);
            return commande;
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
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer la commande par son identifiant unique.");
            }
            return commande;
        }
        public Commande[] GetStaffCommandes(Staff Staff)
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
                    cmd.Parameters.AddWithValue("@ID", Staff.ID);
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
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer les commandes du staff.");
            }
            return commandes.ToArray();
        }
        public Commande[] GetClientCommandes(Client Client)
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
                    cmd.Parameters.AddWithValue("@ID", Client.ID);
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
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer les commandes du client.");
            }
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
                    Commande commande = GetCommande(newid);
                    PlatDB.SetCommandePlats(commande, Commande.Plats);
                    commande.Plats = PlatDB.GetCommandePlats(commande);
                    return commande;
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible d'ajouter la commande.");
            }
        }
        public void UpdateCommande(Commande Commande)
        {
            PlatDB.SetCommandePlats(Commande, Commande.Plats);
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string heurepaiement = string.Empty;
                    string query = @"update Commande set staID=@sta, cliID=@cli, comHeure=@h, comHeureLivraison=@hl, {0}comSomme=@som, comAnnule=@ca 
                                            where comID=@ID";
                    if (Commande.HeurePaiement > DateTime.MinValue) { heurepaiement = "comHeurePaiement=@hp, "; }
                    query = string.Format(query, heurepaiement);
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Commande.ID);
                    cmd.Parameters.AddWithValue("@sta", Commande.Staff.ID);
                    cmd.Parameters.AddWithValue("@cli", Commande.Client.ID);
                    cmd.Parameters.AddWithValue("@h", Commande.Heure);
                    cmd.Parameters.AddWithValue("@hl", Commande.HeureLivraison);
                    if (Commande.HeurePaiement > DateTime.MinValue) { cmd.Parameters.AddWithValue("@hp", Commande.HeurePaiement); }
                    cmd.Parameters.AddWithValue("@som", Commande.Somme);
                    cmd.Parameters.AddWithValue("@ca", Commande.Annule ? "1" : "0");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de mettre à jour la commande.");
            }
        }
    }
}
