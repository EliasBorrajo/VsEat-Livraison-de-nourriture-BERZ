using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class PlatDB : IPlatDB
    {
        private IConfiguration Configuration { get; }
        private IRestaurantDB RestaurantDB { get; }

        public PlatDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            RestaurantDB = new RestaurantDB(Configuration);
        }

        public Plat[] GetRestaurantPlats(int ID)
        {
            List<Plat> plats = new List<Plat>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select platID, resID, platNom, platPrix, platDescription from Plat where resID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string description = string.Empty;
                            if (dr["platDescription"] != DBNull.Value) { description = (string)dr["platDescription"]; }
                            //Image image = null;
                            //if (dr["platImage"] != DBNull.Value) { image = (Image)dr["platImage"]; }
                            plats.Add(new Plat(
                                (int)dr["platID"],
                                RestaurantDB.GetRestaurant((int)dr["resID"]),
                                (string)dr["platNom"],
                                (double)dr["platPrix"],
                                description
                                //,image
                                ));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return plats.ToArray();
        }
        public CommandePlat[] GetCommandePlats(int ID)
        {
            List<CommandePlat> plats = new List<CommandePlat>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select cpID, comID, platID, cpQuantite from CommandePlat where comID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            plats.Add(new CommandePlat((int)dr["cpID"], GetPlat((int)dr["platID"]), (int)dr["cpQuantite"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return plats.ToArray();
        }
        public Plat GetPlat(int ID)
        {
            Plat plat = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select platID, resID, platNom, platPrix, platDescription from Plat where platID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string description = string.Empty;
                            if (dr["platDescription"] != DBNull.Value) { description = (string)dr["platDescription"]; }
                            //Image image = null;
                            //if (dr["platImage"] != DBNull.Value) { image = (Image)dr["platImage"]; }
                            plat = new Plat(
                                (int)dr["platID"],
                                RestaurantDB.GetRestaurant((int)dr["resID"]),
                                (string)dr["platNom"],
                                (double)dr["platPrix"],
                                description
                                //,image
                                );
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return plat;
        }
    }
}
