using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StaffDB : IStaffDB
    {
        private IConfiguration Configuration { get; }

        public StaffDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
    }
}
