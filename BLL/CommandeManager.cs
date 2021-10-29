using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CommandeManager : ICommandeManager
    {
        private ICommandeDB CommandeDB { get; }

        public CommandeManager(IConfiguration Configuration)
        {
            CommandeDB = new CommandeDB(Configuration);
        }
    }
}
