using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class d_PlatManager : d_IPlatManager
    {
        private IPlatDB PlatDB { get; }

        public d_PlatManager(IConfiguration Configuration)
        {
            PlatDB = new PlatDB(Configuration);
        }
    }
}
