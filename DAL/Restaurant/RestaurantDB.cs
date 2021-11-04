using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class RestaurantDB : IRestaurantDB
    {
        private IConfiguration Configuration { get; }
        private ILocaliteDB LocaliteDB { get; }
        private IPlatDB PlatDB { get; }
        
        public RestaurantDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
            PlatDB = new PlatDB(Configuration);
        }
        private Restaurant GetRestaurantFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["resID"];
            Localite localite = LocaliteDB.GetLocalite((int)dr["locID"]);
            string nom = (string)dr["resNom"];
            string adresse = (string)dr["resAdresse"];
            Plat[] plats = PlatDB.GetRestaurantPlats(id);
            return new Restaurant(id, localite, nom, adresse, plats);
        }
        public Restaurant[] GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select resID, locID, resNom, resAdresse 
                                            from Restaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            restaurants.Add(GetRestaurantFromDataReader(dr));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return restaurants.ToArray();
        }
        public Restaurant GetRestaurant(int ID)
        {
            Restaurant restaurant = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select resID, locID, resNom, resAdresse 
                                            from Restaurant where resID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = GetRestaurantFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return restaurant;
        }
    }
}
