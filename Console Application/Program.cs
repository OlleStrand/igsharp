using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Application.Singletons;
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
            Console.WriteLine($"Bank Balance: {igService.AccountDetails.AccountInfo.Balance} {igService.AccountDetails.CurrencyIsoCode}");

            IGMarketDataStreamer streamer = new IGMarketDataStreamer(igService.AccountDetails, igService.Account);

            Console.ReadKey();
            Console.WriteLine($"Order Size: {igService.TradeService.CalculateBuyOrderSize("SEK", 0.0075m)}");

            Console.ReadKey();
            streamer.CloseConnection();
        }
    }
}
