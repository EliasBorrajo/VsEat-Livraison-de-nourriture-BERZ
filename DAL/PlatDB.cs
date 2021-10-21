using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PlatDB : IPlatDB
    {
        private IConfiguration Configuration { get; }
        private RestaurantDB RestaurantDB { get; }

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
                            plats.Add(new Plat(
                                (int)dr["platID"],
                                RestaurantDB.GetRestaurant((int)dr["resID"]),
                                (string)dr["platNom"],
                                (double)dr["platPrix"],
                                (string)dr["platDescription"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return plats.ToArray();
        }
    }
}
