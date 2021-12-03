using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Classe qui implémente l'interface IPlatDB, permettant la récupération et la modification des informations de la table Plat.
    /// </summary>
    public class PlatDB : IPlatDB
    {
        /// <summary>
        /// Objet de configuration permettant la récupération de la chaîne de connexion à la DB.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructeur pour créer un objet PlatDB.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public PlatDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        /// <summary>
        /// Méthode permettant de générer un objet de type Plat depuis un SqlDataReader.
        /// </summary>
        /// <param name="dr">SqlDataReader provenant d'une requête select sur la table Plat.</param>
        /// <returns>Objet de type Plat contenant les informations récupérées depuis la table Plat.</returns>
        private Plat GetPlatFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["platID"];
            string nom = (string)dr["platNom"];
            double prix = (double)dr["platPrix"];
            string description = string.Empty;
            if (dr["platDescription"] != DBNull.Value) { description = (string)dr["platDescription"]; }
            Image image = null;
            if (dr["platImage"] != DBNull.Value) { image = Image.FromStream(new MemoryStream((byte[])dr["platImage"])); }
            return new Plat(id, nom, prix, description, image);
        }
        public Plat GetPlat(int ID)
        {
            Plat plat = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select platID, resID, platNom, platPrix, platDescription, platImage 
                                            from Plat 
                                            where platID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            plat = GetPlatFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer le plat par son identifiant unique.");
            }
            return plat;
        }
        public Plat[] GetRestaurantPlats(Restaurant Restaurant)
        {
            List<Plat> plats = new List<Plat>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select platID, resID, platNom, platPrix, platDescription, platImage 
                                            from Plat 
                                            where resID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Restaurant.ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            plats.Add(GetPlatFromDataReader(dr));
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer les plats du restaurant.");
            }
            return plats.ToArray();
        }
        public CommandePlat[] GetCommandePlats(Commande Commande)
        {
            List<CommandePlat> plats = new List<CommandePlat>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select comID, platID, cpQuantite 
                                            from CommandePlat 
                                            where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Commande.ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            plats.Add(new CommandePlat(GetPlat((int)dr["platID"]), (int)dr["cpQuantite"]));
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer les plats de la commande.");
            }
            return plats.ToArray();
        }
        public void SetCommandePlats(Commande Commande, CommandePlat[] Plats)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            CommandePlat[] currCmdPlats = GetCommandePlats(Commande);
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    foreach (CommandePlat plat in currCmdPlats)
                    {
                        if (!Plats.Contains(plat))
                        {
                            string query = @"delete from CommandePlat
                                                    where comID=@cid and platID=@pid";
                            SqlCommand cmd = new SqlCommand(query, cn);
                            cmd.Parameters.AddWithValue("@cid", Commande.ID);
                            cmd.Parameters.AddWithValue("@pid", plat.ID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    foreach (CommandePlat plat in Plats)
                    {
                        if (!currCmdPlats.Contains(plat))
                        {
                            string query = @"insert into CommandePlat (comID, platID, cpQuantite)
                                            values (@cid, @pid, @qty)";
                            SqlCommand cmd = new SqlCommand(query, cn);
                            cmd.Parameters.AddWithValue("@cid", Commande.ID);
                            cmd.Parameters.AddWithValue("@pid", plat.ID);
                            cmd.Parameters.AddWithValue("@qty", plat.Quantite);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de mettre à jour les plats de la commande.");
            }
        }
    }
}
