using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CommandeManager
    {
        private ICommandeDB CommandeDB { get; }

        public CommandeManager(IConfiguration Configuration)
        {
            CommandeDB = new CommandeDB(Configuration);
        }
    }
}
