﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommandeDB : ICommandeDB
    {
        private IConfiguration Configuration { get; }

        public CommandeDB(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
    }
}
