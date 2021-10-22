using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class StaffManager
    {
        private IStaffDB StaffDB { get; }

        public StaffManager(IConfiguration Configuration)
        {
            StaffDB = new StaffDB(Configuration);
        }
    }
}
