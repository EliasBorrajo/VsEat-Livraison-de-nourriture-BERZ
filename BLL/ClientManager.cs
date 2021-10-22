using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;

namespace BLL
{
    public class ClientManager
    {
        private IClientDB ClientDB { get; }

        public ClientManager(IConfiguration Configuration)
        {
            ClientDB = new ClientDB(Configuration);
        }
    }
}
