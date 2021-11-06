using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// Classe qui implémente l'interface IStaffDB, permettant la récupération et la modification des informations de la table Staff.
    /// </summary>
    public class StaffDB : IStaffDB
    {
        /// <summary>
        /// Objet de configuration permettant la récupération de la chaîne de connexion à la DB.
        /// </summary>
        private IConfiguration Configuration { get; }
        /// <summary>
        /// Objet permettant de récupérer des objets de type Localite.
        /// </summary>
        private ILocaliteDB LocaliteDB { get; }

        /// <summary>
        /// Constructeur pour créer un objet StaffDB.
        /// </summary>
        /// <param name="Configuration">Objet de configuration contenant la chaîne de connexion à la DB.</param>
        public StaffDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
        }
        /// <summary>
        /// Méthode permettant de générer un objet de type Staff depuis un SqlDataReader.
        /// </summary>
        /// <param name="dr">SqlDataReader provenant d'une requête select sur la table Staff.</param>
        /// <returns>Objet de type Staff contenant les informations récupérées depuis la table Staff.</returns>
        private Staff GetStaffFromDataReader(SqlDataReader dr)
        {
            int id = (int)dr["staID"];
            string nom = (string)dr["staNom"];
            string prenom = (string)dr["staPrenom"];
            string telephone = (string)dr["staTelephone"];
            string mail = (string)dr["staMail"];
            string password = (string)dr["staPassword"];
            Localite[] localites = new Localite[] { };
            bool status = (byte)dr["staStatus"] == 1;
            Staff staff = new Staff(id, nom, prenom, telephone, mail, password, localites, status);
            staff.Localites = LocaliteDB.GetStaffLocalites(staff);
            return staff;
        }
        public Staff GetStaff(int ID)
        {
            Staff staff = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select staID, staNom, staPrenom, staTelephone, staMail, staPassword, staStatus 
                                            from Staff 
                                            where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            staff = GetStaffFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staff;
        }
        public Staff GetStaff(string Mail, string Password)
        {
            Staff staff = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select staID, staNom, staPrenom, staTelephone, staMail, staPassword, staStatus 
                                            from Staff 
                                            where staMail=@staMail and staPassword=@staPassword";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@staMail", Mail);
                    cmd.Parameters.AddWithValue("@staPassword", Password);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            staff = GetStaffFromDataReader(dr);
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staff;
        }
        public Staff[] GetStaffWorkingIn(Localite Localite)
        {
            List<Staff> staffs = new List<Staff>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"select staID, locID
                                            from StaffLocalite
                                            where locID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Localite.ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            staffs.Add(GetStaff((int)dr["staID"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staffs.ToArray();
        }
        public Staff AddStaff(Staff Staff)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"insert into Staff (staNom, staPrenom, staTelephone, staMail, staPassword, staStatus) 
                                            values (@staNom, @staPrenom, @staTelephone, @staMail, @staPassword, @staStatus);
                                            select scope_identity()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@staNom", Staff.Nom);
                    cmd.Parameters.AddWithValue("@staPrenom", Staff.Nom);
                    cmd.Parameters.AddWithValue("@staTelephone", Staff.Telephone);
                    cmd.Parameters.AddWithValue("@staMail", Staff.Mail);
                    cmd.Parameters.AddWithValue("@staPassword", Staff.Password);
                    cmd.Parameters.AddWithValue("@staStatus", Staff.Status);
                    cn.Open();
                    int newid = Convert.ToInt32(cmd.ExecuteScalar());
                    Staff staff = GetStaff(newid);
                    LocaliteDB.SetStaffLocalites(staff, Staff.Localites);
                    staff.Localites = LocaliteDB.GetStaffLocalites(staff);
                    return staff;
                }
            }
            catch (Exception e) { throw e; }
        }
        public void UpdateStaff(Staff Staff)
        {
            LocaliteDB.SetStaffLocalites(Staff, Staff.Localites);
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = @"update Staff set staNom=@nom, staPrenom=@pre, staTelephone=@tel, staMail=@mai, staPassword=@pas, staStatus=@sta 
                                            where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Staff.ID);
                    cmd.Parameters.AddWithValue("@nom", Staff.Nom);
                    cmd.Parameters.AddWithValue("@pre", Staff.Prenom);
                    cmd.Parameters.AddWithValue("@tel", Staff.Telephone);
                    cmd.Parameters.AddWithValue("@mai", Staff.Mail);
                    cmd.Parameters.AddWithValue("@pas", Staff.Password);
                    cmd.Parameters.AddWithValue("@sta", Staff.Status);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}
