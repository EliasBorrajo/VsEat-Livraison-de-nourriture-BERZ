using System;

namespace PlatManagementTool
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils cu = new ConsoleUtils();
            cu.PrintRestaurantList();
            cu.PrintInstructions();
            string cmd = Console.ReadLine();
            while (!cmd.ToLower().Equals("exit"))
            {
                Console.WriteLine(cu.Execute(cmd));
                cmd = Console.ReadLine();
            }
        }
    }
}
