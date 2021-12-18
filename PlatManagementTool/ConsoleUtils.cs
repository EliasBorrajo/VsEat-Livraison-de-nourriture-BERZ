using System;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PlatManagementTool
{
    public class ConsoleUtils
    {
        private string Add { get; }
        private string Clear { get; }
        private string Plat { get; }
        private string Restaurant { get; }
        private string[] Instructions { get; }
        private DBUtils dbUtils { get; }

        public ConsoleUtils()
        {
            dbUtils = new DBUtils();
            Add = "add";
            Clear = "clear";
            Plat = "-p";
            Restaurant = "-r";
            Instructions = new string[]
            {
                "----- INFOS -----",
                "(X) si l'image est définie",
                "( ) si l'image n'est pas définie",
                string.Empty,
                "----- UTILISATION -----",
                "ajouter une image\t:\tadd [ID] C:\\Path\\To\\Image.png",
                "supprimer une image :\tclear [ID]",
                " --- ajouter -R après add ou clear pour les restaurants",
                " --- ajouter -P après add ou clear pour les plats",
                "quitter l'application\t\t:\texit"
            };
        }

        public void PrintRestaurantList()
        {
            Restaurant[] restaurants = dbUtils.GetRestaurants();
            foreach (Restaurant restaurant in restaurants)
            {
                string infoImg = string.IsNullOrEmpty(restaurant.ImageBase64) ? " " : "X";
                Console.WriteLine($"[{restaurant.ID}] ({infoImg}) : {restaurant.Nom}");
                foreach (Plat plat in restaurant.Plats)
                {
                    infoImg = string.IsNullOrEmpty(plat.ImageBase64) ? " " : "X";
                    Console.WriteLine($" - [{plat.ID}] ({infoImg})\t:\t{plat.Nom}");
                }
            }
        }

        public void PrintInstructions()
        {
            foreach (string instruction in Instructions)
            {
                Console.WriteLine(instruction);
            }
        }

        public string Execute(string cmd)
        {
            string rv = string.Empty;
            int id = ReadId(cmd);
            if (cmd.ToLower().Contains(Restaurant))
            {
                Restaurant restaurant = null;
                if (cmd.ToLower().StartsWith(Add))
                {
                    restaurant = dbUtils.UpdateRestaurant(id, new Bitmap(ReadPath(cmd)));
                }
                else if (cmd.ToLower().StartsWith(Clear))
                {
                    restaurant = dbUtils.UpdateRestaurant(id, null);
                }
                string infoImg = string.IsNullOrEmpty(restaurant.ImageBase64) ? " " : "X";
                rv = $"[{restaurant.ID}] ({infoImg}) : {restaurant.Nom}";
            }
            else if (cmd.ToLower().Contains(Plat))
            {
                Plat plat = null;
                if (cmd.ToLower().StartsWith(Add))
                {
                    plat = dbUtils.UpdatePlat(id, new Bitmap(ReadPath(cmd)));
                }
                else if (cmd.ToLower().StartsWith(Clear))
                {
                    plat = dbUtils.UpdatePlat(id, null);
                }
                string infoImg = string.IsNullOrEmpty(plat.ImageBase64) ? " " : "X";
                rv = $" - [{plat.ID}] ({infoImg})\t:\t{plat.Nom}";
            }
            return rv;
        }

        private int ReadId(string cmd)
        {
            int rv = -1;
            string[] split = cmd.Split(" ");
            if (split.Length > 2)
            {
                int.TryParse(split[2], out rv);
            }
            return rv;
        }

        private string ReadPath(string cmd)
        {
            string path = string.Empty;
            string[] split = cmd.Split(" ");
            if (split.Length > 3)
            {
                path = split[3];
            }
            return path;
        }
    }
}
