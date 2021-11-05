using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// Classe qui implémente l'interface ILocaliteDB, permettant la récupération et la modification des informations de la table Localite.
    /// </summary>
    public class LocaliteDB : ILocaliteDB
    {
        /// <summary>
        /// Objet de configuration permettant la récupération de la chaîne de connexion à la DB.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructeur pour créer un objet LocaliteDB.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public LocaliteDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        /// <summary>
        /// Méthode permettant de générer un objet de type Localite depuis un SqlDataReader.
        /// </summary>
        /// <param name="dr">SqlDataReader provenant d'une requête select sur la table Localite.</param>
        /// <returns>Objet de type Localite contenant les informations récupérées depuis la table Localite.</returns>
        private Localite GetLocaliteFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["locID"];
            string nom = (string)dr["locNom"];
            string npa = (string)dr["locNPA"];
            return new Localite(id, nom, npa);
        }
        public Localite GetLocalite(int ID)
        {
            Localite localite = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select locID, locNom, locNPA 
                                            from Localite 
                                            where locID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            localite = GetLocaliteFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return localite;
        }
        public Localite[] GetStaffLocalites(int ID)
        {
            List<Localite> localites = new List<Localite>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select staID, locID 
                                            from StaffLocalite 
                                            where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            localites.Add(GetLocalite((int)dr["locID"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return localites.ToArray();
        }
        public Localite[] GetLocalites()
        {
            List<Localite> localites = new List<Localite>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select locID, locNom, locNPA 
                                            from Localite";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            localites.Add(GetLocaliteFromDataReader(dr));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return localites.ToArray();
        }
        public void SetStaffLocalites(int ID, Localite[] Localites)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Localite[] currStaffLocs = GetStaffLocalites(ID);
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    foreach (Localite localite in currStaffLocs)
                    {
                        if (!Localites.Contains(localite))
                        {
                            string query = @"delete from StaffLocalite
                                                    where staID=@sid and locID=@lid";
                            SqlCommand cmd = new SqlCommand(query, cn);
                            cmd.Parameters.AddWithValue("@sid", ID);
                            cmd.Parameters.AddWithValue("@lid", localite.ID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    foreach (Localite localite in Localites)
                    {
                        if (!currStaffLocs.Contains(localite))
                        {
                            string query = @"insert into StaffLocalite (staID, locID) 
                                                values (@staID, @locID)";
                            SqlCommand cmd = new SqlCommand(query, cn);
                            cmd.Parameters.AddWithValue("@staID", ID);
                            cmd.Parameters.AddWithValue("@locID", localite.ID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}
