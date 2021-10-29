using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class PlatManager : IPlatManager
    {
        private IPlatDB PlatDB { get; }

        public PlatManager(IConfiguration Configuration)
        {
            PlatDB = new PlatDB(Configuration);
        }
    }
}
