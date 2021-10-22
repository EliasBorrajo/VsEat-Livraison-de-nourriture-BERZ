using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LocaliteManager
    {
        private ILocaliteDB LocaliteDB { get; }

        public LocaliteManager(IConfiguration Configuration)
        {
            LocaliteDB = new LocaliteDB(Configuration);
        }
    }
}
