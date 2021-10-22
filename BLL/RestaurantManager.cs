using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RestaurantManager
    {
        private IRestaurantDB RestaurantDB { get; }

        public RestaurantManager(IConfiguration Configuration)
        {
            RestaurantDB = new RestaurantDB(Configuration);
        }
    }
}
