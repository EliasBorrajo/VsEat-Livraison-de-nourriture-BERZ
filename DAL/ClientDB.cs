using Microsoft.Extensions.Configuration;
using System;

namespace DAL
{
    public class ClientDB : IClientDB
    {
        private IConfiguration Configuration { get; }

        public ClientDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
    }
}
