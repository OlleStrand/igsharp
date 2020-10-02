﻿using IgBotTraderCLI.Services;
using System;

namespace IgBotTraderCLI
{
    internal class Program
    {
        private static IGMarketDataStreamer streamer;

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramExit);

            HttpIGAccountService igService = new HttpIGAccountService(ConfigurationService.LoadAccountDetails(), true);
            Console.WriteLine($"Bank Balance: {igService.AccountDetails.AccountInfo.Balance} {igService.AccountDetails.CurrencyIsoCode}");

            igService.TradeService.SetAccountServiceToMain();
            streamer = new IGMarketDataStreamer(igService.AccountDetails, igService.Account);

            Console.ReadKey();
            Console.WriteLine($"Order Size: {igService.TradeService.CalculateBuyOrderSize("SEK", 0.0075m, 0.25m, 200)}");

            Console.ReadKey();
        }

        private static void ProgramExit(object sender, EventArgs e)
        {
            try
            {
                if (streamer != null)
                {
                    streamer.CloseConnection();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}