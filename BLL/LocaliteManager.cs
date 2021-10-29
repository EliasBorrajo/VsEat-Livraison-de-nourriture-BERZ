using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LocaliteManager : ILocaliteManager
    {
        private ILocaliteDB LocaliteDB { get; }

        public LocaliteManager(IConfiguration Configuration)
        {
            LocaliteDB = new LocaliteDB(Configuration);
        }

        public Localite[] GetLocalites()
        {
            return LocaliteDB.GetLocalites();
        }
    }
}
