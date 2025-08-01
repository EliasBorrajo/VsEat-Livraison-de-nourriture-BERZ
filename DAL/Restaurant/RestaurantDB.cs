﻿using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    /// <summary>
    /// Classe qui implémente l'interface IRestaurantDB, permettant la récupération et la modification des informations de la table Restaurant.
    /// </summary>
    public class RestaurantDB : IRestaurantDB
    {
        /// <summary>
        /// Objet de configuration permettant la récupération de la cha'ine de connexion à la DB.
        /// </summary>
        private IConfiguration Configuration { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Localite.
        /// </summary>
        private ILocaliteDB LocaliteDB { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Plat.
        /// </summary>
        private IPlatDB PlatDB { get; }
        
        /// <summary>
        /// Constructeur pour créer un objet RestaurantDB.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public RestaurantDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
            PlatDB = new PlatDB(Configuration);
        }
        /// <summary>
        /// Méthode permettant de générer un objet de type Restaurant depuis un SqlDataReader.
        /// </summary>
        /// <param name="dr">SqlDataReader provenant d'une requête select sur la table Restaurant.</param>
        /// <returns>Objet de type Restaurant contenant les informations récupérées depuis la table Restaurant.</returns>
        private Restaurant GetRestaurantFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["resID"];
            Localite localite = LocaliteDB.GetLocalite((int)dr["locID"]);
            string nom = (string)dr["resNom"];
            string adresse = (string)dr["resAdresse"];
            Plat[] plats = new Plat[] { };
            string image = null;
            if (dr["resImage"] != DBNull.Value) { try { image = Convert.ToBase64String(new MemoryStream((byte[])dr["resImage"]).ToArray()); } catch { } }
            Restaurant restaurant = new Restaurant(id, localite, nom, adresse, plats, image);
            restaurant.Plats = PlatDB.GetRestaurantPlats(restaurant);
            return restaurant;
        }
        public Restaurant[] GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select resID, locID, resNom, resAdresse, resImage 
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
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer tous les restaurants.");
            }
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
                    string query = @"select resID, locID, resNom, resAdresse, resImage 
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
            catch (Exception e) 
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer le restaurant par son identifiant unique.");
            }
            return restaurant;
        }
        public Restaurant GetRestaurantByPlat(int ID)
        {
            Restaurant restaurant = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select resID
                                        from Plat
                                        where platID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = GetRestaurant((int)dr["resID"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException(e.Message, "Impossible de récupérer le restaurant de la commmande.");
            }
            return restaurant;
        }
    }
}
