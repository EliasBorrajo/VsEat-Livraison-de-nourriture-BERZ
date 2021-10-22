using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class PlatManager
    {
        private IPlatDB PlatDB { get; }

        public PlatManager(IConfiguration Configuration)
        {
            PlatDB = new PlatDB(Configuration);
        }
    }
}
