using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LocaliteManager : IPlatManager
    {
        private ILocaliteDB LocaliteDB { get; }

        public LocaliteManager(IConfiguration Configuration)
        {
            LocaliteDB = new LocaliteDB(Configuration);
        }
    }
}
