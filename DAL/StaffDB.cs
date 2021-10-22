using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StaffDB : IStaffDB
    {
        private IConfiguration Configuration { get; }
        private LocaliteDB LocaliteDB { get; }

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
    }
}
