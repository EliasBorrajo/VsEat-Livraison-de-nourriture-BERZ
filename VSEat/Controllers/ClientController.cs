using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSEat.Models;

namespace VSEat.Controllers
{
    public class ClientController : Controller
    {
        private ClientDetailVM client = new ClientDetailVM(string.Empty, string.Empty, string.Empty, new LocaliteVM("Sion", "1950"), new LocaliteVM[] { new LocaliteVM("Sion", "1950"), new LocaliteVM("Sierre", "3960") });

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(ClientDetailVM client)
        {
            this.client = client;
            return View();
        }
    }
}
