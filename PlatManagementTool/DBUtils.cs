using BLL;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace PlatManagementTool
{
    public class DBUtils
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        private string ConnectionString
        {
            get { return Configuration.GetConnectionString("DefaultConnection"); }
        }
        private IRestaurantManager RestaurantManager { get; }
        private enum UpdateType { Restaurant, Plat };

        public DBUtils()
        {
            RestaurantManager = new RestaurantManager(new RestaurantDB(Configuration));
        }

        public Restaurant[] GetRestaurants()
        {
            return RestaurantManager.GetRestaurants();
        }

        public Restaurant GetRestaurant(int id)
        {
            return RestaurantManager.GetRestaurant(id);
        }

        public Plat GetPlat(int id)
        {
            Plat plat = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select platID, platNom, platImage 
                                            from Plat 
                                            where platID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string img = null;
                            if (dr["platImage"] != DBNull.Value) { try { img = Convert.ToBase64String(new MemoryStream((byte[])dr["platImage"]).ToArray()); } catch { } }
                            plat = new Plat((int)dr["platID"], (string)dr["platNom"], -1, string.Empty, img);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return plat;
        }

        public Restaurant UpdateRestaurant(int id, Image img)
        {
            Update(UpdateType.Restaurant, id, img);
            return GetRestaurant(id);
        }

        public Plat UpdatePlat(int id, Image img)
        {
            Update(UpdateType.Plat, id, img);
            return GetPlat(id);
        }

        private void Update(UpdateType updateType, int id, Image img)
        {
            string query = string.Empty;
            if (updateType == UpdateType.Plat)
            {
                query = @"update Plat set platImage=@img where platID=@ID";
            }
            else
            {
                query = @"update Restaurant set resImage=@img where resID=@ID";
            }
            if (!string.IsNullOrEmpty(query))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.Add("@img", System.Data.SqlDbType.VarBinary, -1);
                        cmd.Parameters["@img"].Value = img == null ? DBNull.Value : ImageToByteArray(img);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn == null) { return null; }
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
