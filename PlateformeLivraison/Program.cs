using BLL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace PlateformeLivraison
{
    class Program
    {
        //*******************************************************************************************************
        // C O N F I G U R A T I O N
        //*******************************************************************************************************
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        //*******************************************************************************************************
        // A T T R I B U T S
        //*******************************************************************************************************
        private static IClientManager              clientManager;
        private static ICommandeManager     commandeManager;
        private static ILocaliteManager           localiteManager;
        private static IRestaurantManager      restaurantManager;
        private static IStaffManager                staffManager;

        static Program()
        {
            clientManager = new ClientManager(Configuration);
            restaurantManager = new RestaurantManager(Configuration);
            commandeManager = new CommandeManager(Configuration);
            localiteManager = new LocaliteManager(Configuration);
            staffManager = new StaffManager(Configuration);
        }
        //*******************************************************************************************************
        // M A I N
        //*******************************************************************************************************
        static void Main(string[] args)
        {
            Console.WriteLine("SCENARIO : CLIENT PASSE COMMANDE");
            Console.WriteLine("*************************************************************************************");
            scenClientPasseCommande( );
            Console.WriteLine("*************************************************************************************");


            Console.WriteLine("SCENARIO : CONNECTION STAFF");
            Console.WriteLine("*************************************************************************************");
            scenConnectionStaff();
            Console.WriteLine("*************************************************************************************");

        }


        //*******************************************************************************************************
        // S C E N A R I O S
        //*******************************************************************************************************
        private static void scenClientPasseCommande()
        {
            // 1)  Le client se connecte
            string cmail = "sanglier.cornouaille@dies.irae";
            string cpass = "4200";
            Console.WriteLine($"Connexion client avec les identifiants : {cmail} - {cpass}");
            Client client = clientManager.GetClient(cmail, cpass);
            if (client != null)
            {
                Console.WriteLine($"Connexion réussie, bienvenue {client.Prenom} {client.Nom} votre localité actuelle : { client.Localite.Nom}!");
            }
            else
            {
                Console.WriteLine($"Connexion échouée, vérifiez l'identifiant et le mot de passe.");
            }

            // 2) le système affiche un choix des restaurants proches
           Restaurant[] restaurantsArr =  affcheRestaurants( client.Localite.ID );
           
            // 3) Le client choisit son restaurant parmi une liste (Tri par région, plus proche au plus loin)
            int userSelectedRestaurant = 2;
            affcheRestaurantSelectionne( userSelectedRestaurant , restaurantsArr);

            // Le client choisit la composition de sa commande.
            List<CommandePlat> platsSelectionneesParUser = userSelectsPlats(restaurantsArr, userSelectedRestaurant);

            // 4) Système vérifie que il y ait un staff disponible pour cette tranche d'heure --> dans AddCommande
            // Si pas dispo, proposer au client d'entrer une autre heure
            // 5) Si Staff dispo dans même ville que restaurant, lui ajouter la commande --> Deja fait dans AddCommande
            // Ajouter 5 commandes max toutes les 30 min max au staff. --> BLL
            DateTime heureLivraisonClient = DateTime.Now;
            heureLivraisonClient = heureLivraisonClient.AddHours(4);    // Viens dans 4 heures bro
            Restaurant resto = restaurantsArr[userSelectedRestaurant];
            CommandePlat[] tab = platsSelectionneesParUser.ToArray();
            Commande clientSelection = commandeManager.AddCommande( client, resto , tab , heureLivraisonClient );

            // La commande est null si le staff est pas dispo
            if (clientSelection == null)
            {
                Console.WriteLine("Aucun staff n'est disponible dans cette ville & restaurent aux heures selectinnées. Veuillez choisir UNE AUTRE HEURE.");
            }
            else
            {
                Console.WriteLine("La commande arrivera pour : "+heureLivraisonClient+". La somme de votre commande est de : "+clientSelection.Somme);
            }

            //commandeManager.CancelCommande(clientSelection.ID, client.Nom, client.Prenom);

            // 6A) Le client reçoit la commande, la commande est confirmé par l'heure de payement. --> Commande confirmé par staff --> BLL Commande
            if (clientSelection.Annule == false)
            {
                Console.WriteLine("Commande validé payement !");
                commandeManager.ValidatePayment( clientSelection );
            }
            else // 6B) Le client annule sa commande
            {
                Console.WriteLine("Comamnde annulé");
                commandeManager.CancelCommande( clientSelection.ID, client.Nom, client.Prenom );
            }
            
        }

         private static void scenConnectionStaff()
        {
            // STAFF CONNECTION
            string smail = "esclaves.pascher@dies.irae";
            string spass = "3337";
            Console.WriteLine($"Connexion staff avec les identifiants : {smail} - {spass}");
            Staff staff = staffManager.GetStaff(smail, spass);

            if (staff != null)
            {
                Console.WriteLine($"Connexion réussie, bienvenue {staff.Prenom} {staff.Nom}");
                Localite[] localitesArr = localiteManager.GetLocalites();
  
                PrintStaffLocs(staff);                     // Affiche les localitées actuelles
                staff.Localites = new Localite[]
                {
                    localitesArr[5],
                    localitesArr[6]
                };
                staffManager.UpdateStaff(staff); // Supprime les anciennes localités, et affiche les localitées actuelles
                PrintStaffLocs(staff);

                // Récuperer les commandes EN COURS du staff et les afficher
                Commande[] scommandes = commandeManager.GetStaffCommandes(staff, true);
                Console.WriteLine($"Commandes en cours du staff {staff.Nom} {staff.Prenom} : ");
                PrintCommandes(scommandes);

            }
            else
            {
                Console.WriteLine($"Connexion échouée, vérifiez l'identifiant et le mot de passe.");
            }
        }

        //*******************************************************************************************************
        // M E T H O D E S
        //*******************************************************************************************************
        private static void PrintStaffLocs(Staff staff)
        {
            Console.WriteLine($"{staff.Prenom} travaille dans les localités suivantes : ");
            foreach (Localite loc in staff.Localites)
            {
                Console.WriteLine($"- {loc.NPA} {loc.Nom}");
            }
        }
        private static void PrintCommandes(Commande[] commandes)
        {
            foreach (Commande commande in commandes)
            {
                Console.WriteLine("-----");
                Console.WriteLine($"Commande no{commande.ID} :");
                Console.WriteLine($"Attribuée à {commande.Staff.Nom} {commande.Staff.Prenom}");
                Console.WriteLine($"Livrer à {commande.Client.Nom} {commande.Client.Prenom} - {commande.Client.Adresse} ({commande.Client.Localite.NPA} {commande.Client.Localite.Nom})");
                foreach (CommandePlat commandePlat in commande.Plats)
                {
                    Console.WriteLine($" - {commandePlat.Quantite} {commandePlat.Nom}");
                }
                Console.WriteLine("-----");
            }
        }

        private static List<CommandePlat> userSelectsPlats(Restaurant[] restaurantsArr, int userSelectedRestaurant)
        {
            List<CommandePlat> selectedPlats = new List<CommandePlat>();
            //selectedPlats.Add( new CommandePlat(   restaurantsArr[ userSelectedRestaurant ].Plats[0],   5    ) );
            selectedPlats.Add(new CommandePlat(GetRestaurantByID(userSelectedRestaurant).Plats[2]  ,5 ));       // TODO: Faire avec une methode GetPlatByID au lieu de position du tableau !  
            selectedPlats.Add(new CommandePlat(GetRestaurantByID(userSelectedRestaurant).Plats[3], 2));
            selectedPlats.Add(new CommandePlat(GetRestaurantByID(userSelectedRestaurant).Plats[1], 4));

            Console.WriteLine("Plats sélectionnées : ");
            foreach  (CommandePlat cm in selectedPlats)
            {
                Console.WriteLine("\t --> "+ cm.Nom  );
            }

            return selectedPlats;
        }

        private static Restaurant GetRestaurantByID(int ID)
        {
            Restaurant[] restoArr = restaurantManager.GetRestaurants();

            for (int i = 0; i < restoArr.Length; i++)
            {
                if (ID == restoArr[i].ID)
                {
                    return restoArr[i];
                }

            }

            return null;
        }
        private static void affcheRestaurantSelectionne(int userSelectedRestaurant, Restaurant[] restaurantsArr)
        {
            String restoName = "";
            int posRestaurant = -1;
            for (int i = 0; i < restaurantsArr.Length  ; i++)
            {
                if (userSelectedRestaurant == restaurantsArr[i].ID)
                {
                    restoName = restaurantsArr[i].Nom;
                    posRestaurant = i ;
                }
            
            }

            if (restoName.Equals(""))
            {
                Console.WriteLine("ERROR : Aucun Restaurant pour l'ID sélectionné");
            }
            else
            {
                Console.WriteLine("Restaurant sélectionné : " + restoName);
                affichePlatsRestaurant(restaurantsArr, posRestaurant);
            }
        }

        private static void affichePlatsRestaurant(Restaurant[] restaurantsArr, int posRestaurant)
        {
            int platsLength = restaurantsArr[posRestaurant].Plats.Length;

            Console.WriteLine("Voici les plats que propose le restaurant : ");
            for (int i = 0; i < platsLength  ; i++)
            {
                Console.WriteLine( i+"\t --> "+ restaurantsArr[posRestaurant].Plats[i].Prix +" CHF : "
                                                               + restaurantsArr[posRestaurant].Plats[i].Nom
                                                               + " || --> " + restaurantsArr[posRestaurant].Plats[i].Description);
            }
        }

        private static Restaurant[] affcheRestaurants(int clientID)
        {
            Console.WriteLine("Voici une sélection des restaurants : ");
            Restaurant[] restaurantsArr = restaurantManager.GetRestaurants();

            int i = 1;
            foreach (Restaurant resto in restaurantsArr)
            {
                Console.WriteLine(i + ": "+ resto.Nom + "\t à la localité "+ resto.Localite.ID+" - "+resto.Localite.Nom);
                i++;
            }
            return restaurantsArr;
        }

        private static void oldMain ()
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
                Commande[] cmds = cmdm.GetStaffCommandes(s, true);
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
