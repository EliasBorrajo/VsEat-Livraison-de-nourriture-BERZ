using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RestaurantDB : IRestaurantDB
    {
        private IConfiguration Configuration { get; }
        private LocaliteDB LocaliteDB { get; }
        private PlatDB PlatDB { get; }
        
        public RestaurantDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
            PlatDB = new PlatDB(Configuration);
        }
        public Restaurant[] GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select resID, locID, resNom, resAdresse from Restaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            restaurants.Add(new Restaurant(
                                (int)dr["resID"],
                                LocaliteDB.GetLocalite((int)dr["locID"]),
                                (string)dr["resNom"],
                                (string)dr["resAdresse"],
                                PlatDB.GetRestaurantPlats((int)dr["resID"])));
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
                    string query = "select resID, locID, resNom, resAdresse from Restaurant where resID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = new Restaurant(
                                (int)dr["resID"],
                                LocaliteDB.GetLocalite((int)dr["locID"]),
                                (string)dr["resNom"],
                                (string)dr["resAdresse"],
                                PlatDB.GetRestaurantPlats((int)dr["resID"]));

                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return restaurant;
        }
    }
}
