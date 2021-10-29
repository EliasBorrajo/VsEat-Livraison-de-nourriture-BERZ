using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class StaffDB : IStaffDB
    {
        private IConfiguration Configuration { get; }
        private ILocaliteDB LocaliteDB { get; }

        public StaffDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            LocaliteDB = new LocaliteDB(Configuration);
        }
        public Staff GetStaff(int ID)
        {
            Staff staff = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select staID, locID, staNom, staPrenom, staTelephone, staMail, staPassword from Staff where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            staff = new Staff(
                                (int)dr["staID"],
                                (string)dr["staNom"],
                                (string)dr["staPrenom"],
                                (string)dr["staTelephone"],
                                (string)dr["staMail"],
                                (string)dr["staPassword"],
                                LocaliteDB.GetStaffLocalites((int)dr["staID"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staff;
        }
        public Staff AddStaff(Staff NewStaff)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "insert into Staff (staNom, staPrenom, staTelephone, staMail, staPassword) values (@staNom, @staPrenom, @staTelephone, @staMail, @staPassword);select scope_identity()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@staNom", NewStaff.Nom);
                    cmd.Parameters.AddWithValue("@staPrenom", NewStaff.Nom);
                    cmd.Parameters.AddWithValue("@staTelephone", NewStaff.Telephone);
                    cmd.Parameters.AddWithValue("@staMail", NewStaff.Mail);
                    cmd.Parameters.AddWithValue("@staPassword", NewStaff.Password);
                    cn.Open();
                    int newid = Convert.ToInt32(cmd.ExecuteScalar());
                    return GetStaff(newid);
                }
            }
            catch (Exception e) { throw e; }
        }
        public Staff GetStaff(string Mail, string Password)
        {
            Staff staff = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select staID, locID, staNom, staPrenom, staTelephone, staMail, staPassword from Staff where staMail=@staMail and staPassword=@staPassword";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@staMail", Mail);
                    cmd.Parameters.AddWithValue("@staPassword", Password);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            staff = new Staff(
                                (int)dr["staID"],
                                (string)dr["staNom"],
                                (string)dr["staPrenom"],
                                (string)dr["staTelephone"],
                                (string)dr["staMail"],
                                (string)dr["staPassword"],
                                LocaliteDB.GetStaffLocalites((int)dr["staID"]));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staff;
        }
        public Staff[] GetStaffs()
        {
            List<Staff> staffs = new List<Staff>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select staID, locID, staNom, staPrenom, staTelephone, staMail, staPassword from Staff";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            staffs.Add(new Staff(
                                (int)dr["staID"],
                                (string)dr["staNom"],
                                (string)dr["staPrenom"],
                                (string)dr["staTelephone"],
                                (string)dr["staMail"],
                                (string)dr["staPassword"],
                                LocaliteDB.GetStaffLocalites((int)dr["staID"])));
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staffs.ToArray();
        }
        public void UpdateStaff(Staff Staff)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "update Staff set staNom=@nom, staPrenom=@pre, staTelephone=@tel, staMail=@mai, staPassword=@pas where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Staff.ID);
                    cmd.Parameters.AddWithValue("@nom", Staff.Nom);
                    cmd.Parameters.AddWithValue("@pre", Staff.Prenom);
                    cmd.Parameters.AddWithValue("@tel", Staff.Telephone);
                    cmd.Parameters.AddWithValue("@mai", Staff.Mail);
                    cmd.Parameters.AddWithValue("@pas", Staff.Password);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
        public void DeleteStaff(Staff Staff)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "delete from Staff where staID=@ID";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@ID", Staff.ID);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e) { throw e; }
        }
        public Staff[] GetDispStaffs(Localite Localite)
        {
            List<Staff> staffs = new List<Staff>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select staID, locID, staNom, staPrenom, staTelephone, staMail, staPassword from Staff";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Staff staff = new Staff(
                                (int)dr["staID"],
                                (string)dr["staNom"],
                                (string)dr["staPrenom"],
                                (string)dr["staTelephone"],
                                (string)dr["staMail"],
                                (string)dr["staPassword"],
                                LocaliteDB.GetStaffLocalites((int)dr["staID"]));
                            foreach (Localite localite in staff.Localites)
                            {
                                if (localite.ID == Localite.ID)
                                {
                                    staffs.Add(staff); break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e) { throw e; }
            return staffs.ToArray();
        }
    }
}
