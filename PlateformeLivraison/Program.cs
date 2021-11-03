using BLL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PlateformeLivraison
{
    class Program
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            IRestaurantManager rm = new RestaurantManager(Configuration);
            Restaurant[] restaurants = rm.GetRestaurants();
            foreach (Restaurant resto in restaurants)
            {
                Console.WriteLine($"Restaurant '{resto.Nom}'");
                Console.WriteLine($" {resto.Adresse}");
                Console.WriteLine($" {resto.Localite.NPA} - {resto.Localite.Nom}");
                Plat[] plats = resto.Plats;
                if (plats.Length > 0)
                {
                    Console.WriteLine($" Plats : ");
                }
                foreach (Plat plat in plats)
                {
                    Console.WriteLine($"  - {plat.Nom} - {plat.Description} - {plat.Prix.ToString("N2")} francs");
                }
            }
            IClientManager cm = new ClientManager(Configuration);
            Client c = cm.GetClient("bw@mail.com", "1234");
            if (c != null)
            {
                Console.WriteLine($"Client {c.ID} : {c.Nom}, {c.Prenom}");
                Console.WriteLine($" Adresse : {c.Adresse} - {c.Localite.NPA} {c.Localite.Nom}");
                Console.WriteLine($" Contact : {c.Mail} - {c.Telephone}");
            }
            IStaffManager sm = new StaffManager(Configuration);
            ICommandeManager cmdm = new CommandeManager(Configuration);
            Staff s = sm.GetStaff("pdg@mail.com", "1234");
            if (s != null)
            {
                Commande[] cmds = cmdm.GetStaffCommandes(s.ID, true);
                Console.WriteLine($"{s.Nom}, {s.Prenom} ({s.Telephone} - {s.Mail})");
                Localite[] sl = s.Localites;
                if (sl.Length > 0)
                {
                    Console.WriteLine($"Travaille à :");
                    foreach (Localite localite in sl)
                    {
                        Console.WriteLine(localite.Nom);
                    }
                }
                if (cmds.Length > 0)
                {
                    Console.WriteLine("Commandes en cours : ");
                    foreach (Commande commande in cmds)
                    {
                        Console.WriteLine($"{commande.Client.Nom}, {commande.Client.Prenom} - {commande.Somme.ToString("N2")}");
                    }
                }
            }
        }
    }
}
