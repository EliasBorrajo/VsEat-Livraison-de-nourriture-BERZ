using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using BLL;
using Microsoft.Extensions.Configuration;

namespace PlatManagementTool
{
    class Program
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            PlatImageManager pim = new PlatImageManager(Configuration);
            string[] plats = pim.Plats;
            foreach (string plat in plats)
            {
                Console.WriteLine(plat);
            }
            Console.WriteLine();
            string[] instr = pim.Instructions;
            foreach (string inst in instr)
            {
                Console.WriteLine(inst);
            }
            string cmd = Console.ReadLine();
            while (!cmd.ToLower().Equals("exit"))
            {
                if (cmd.ToLower().StartsWith("add"))
                {
                    string[] split = cmd.Split(" ");
                    if (split.Length == 3)
                    {
                        string path = split[2];
                        if (int.TryParse(split[1], out int id)) 
                        {
                            pim.AddImage(id, path);
                        }
                    }
                }
                else if (cmd.ToLower().StartsWith("clear"))
                {
                    string[] split = cmd.Split(" ");
                    if (split.Length == 2)
                    {
                        if (int.TryParse(split[1], out int id))
                        {
                            pim.RemoveImage(id);
                        }
                    }
                }
                cmd = Console.ReadLine();
            }
        }

        private class PlatImageManager
        {
            private IRestaurantManager RestaurantManager { get; set; }
            public PlatImageManager(IConfiguration Configuration)
            {
                RestaurantManager = new RestaurantManager(new DAL.RestaurantDB(Configuration));
            }
            public string[] Instructions
            {
                get
                {
                    string[] instructions = new string[]
                    {
                        "----- INFOS -----",
                        "(X) si l'image est définie pour ce plat",
                        "( ) si l'image n'est pas définie pour ce plat",
                        "----- UTILISATION -----",
                        "ajouter une image pour un plat\t:\tadd [ID] C:\\Path\\To\\Image.png",
                        "supprimer une image pour un plat:\tclear [ID]",
                        "quitter l'application\t\t:\texit"
                    };
                    return instructions;
                }
            }
            public string[] Plats
            {
                get
                {
                    List<string> plats = new List<string>();
                    DTO.Restaurant[] restaurants = RestaurantManager.GetRestaurants();
                    foreach (DTO.Restaurant restaurant in restaurants)
                    {
                        plats.Add($"[{restaurant.ID}] : {restaurant.Nom}");
                        foreach (DTO.Plat plat in restaurant.Plats)
                        {
                            string infoImg = plat.Image != null ? "X" : " ";
                            plats.Add($" - [{plat.ID}] ({infoImg})\t:\t{plat.Nom}");
                        }
                    }
                    return plats.ToArray();
                }
            }
            public void AddImage(int id, string path)
            {
                DTO.Plat plat = GetPlat(id);
                Image img = new Bitmap(path);
                if (plat != null)
                {
                    Console.WriteLine(UpdatePlat(id, img));
                }
            }
            public void RemoveImage(int id)
            {
                DTO.Plat plat = GetPlat(id);
                if (plat != null)
                {
                    Console.WriteLine(UpdatePlat(id, null));
                }
            }
            private byte[] ImageToByteArray(Image imageIn)
            {
                if (imageIn == null) { return null; }
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            private string UpdatePlat(int id, Image img)
            {
                string rv = " - [{0}] ({1})\t:\t{2}";
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        string query = @"update Plat set platImage=@img where platID=@ID";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@img", img == null ? DBNull.Value : ImageToByteArray(img));
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e) { throw e; }
                DTO.Plat plat = GetPlat(id);
                return string.Format(rv, plat.ID, plat.Image != null ? "(X)" : " ");
            }
            private DTO.Plat GetPlat(int ID)
            {
                DTO.Plat plat = null;
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                try
                {
                    using (SqlConnection cn = new SqlConnection(connectionString))
                    {
                        string query = @"select platID, platImage 
                                            from Plat 
                                            where platID=@ID";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Image img = null;
                                if (dr["platImage"] != DBNull.Value) { img = Image.FromStream(new MemoryStream((byte[])dr["platImage"])); }
                                plat = new DTO.Plat((int)dr["platID"], string.Empty, 0, string.Empty, img);
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
        }
    }
}
