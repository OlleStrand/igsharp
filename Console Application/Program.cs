using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IgBotTraderCLI.Models;
using IgBotTraderCLI.Services;

namespace IgBotTraderCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string username = Console.ReadLine();
            string password = Console.ReadLine();
            string apiKey = Console.ReadLine();
            */

            string username = "ollesapi";
            string password = "Ollesapi_1";
            string apiKey = "77d3fdd8a0fe83431935a9e79e7e6255c05ba115";

            HttpIGAccountService igService = new HttpIGAccountService(new IGApiAccount(username, password, apiKey));

            Console.WriteLine($"{igService.AccountDetails.AccountInfo.Balance} CHF");
            Console.WriteLine($"Order Size: {igService.TradeService.CalculateOrderSize("SEK")}");

            Console.ReadKey();
        }
    }
}
