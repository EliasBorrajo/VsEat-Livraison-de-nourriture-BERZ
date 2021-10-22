using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LocaliteDB : ILocaliteDB
    {
        private IConfiguration Configuration { get; }

        public LocaliteDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public Localite GetLocalite(int ID)
        {
            Localite localite = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select locID, locNom, locNPA from Localite where locID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            localite = new Localite((int)dr["locID"], (string)dr["locNom"], (string)dr["locNPA"]);
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
                    string query = "select staID, locID from StaffLocalite where staID=@ID";
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
                    string query = "select locID, locNom, locNPA from Localite";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            localites.Add(new Localite((int)dr["locID"], (string)dr["locNom"], (string)dr["locNPA"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return localites.ToArray();
        }
    }
}
