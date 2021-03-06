﻿using IgBotTraderCLI.Indicators;
using IgBotTraderCLI.Services;
using System;

namespace IgBotTraderCLI
{
    public class Program
    {
        private static IGMarketDataStreamer streamer;

        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramExit);

            HistoricalDataService dataService = new HistoricalDataService();

            HttpIGAccountService igService = new HttpIGAccountService(ConfigurationService.LoadAccountDetails(), true);
            Console.WriteLine($"Bank Balance: {igService.AccountDetails.AccountInfo.Balance} {igService.AccountDetails.CurrencyIsoCode}");

            IGTradeService tradeService = new IGTradeService();
            tradeService.SetAccountServiceToMain();
            streamer = new IGMarketDataStreamer(igService.AccountDetails, igService.Account);

            dataService.LoadIGHistoricalData("IX.D.OMX.IFM.IP", "MINUTE_15", 51);

            decimal[] values = dataService.OhlcListToArray(51, dataService.HistoricalDataIG);

            var SMA = new SimpleMovingAverage(values, 50).Sma();
            var EMA = new ExponentialMovingAverage(values, 12).Ema();

            //igService.PlaceOrder("IX.D.OMX.IFM.IP", "BUY", 10, "SEK");

            Console.ReadKey();
            Console.WriteLine($"Order Size: {tradeService.CalculateBuyOrderSize("SEK", 0.0075m, 0.25m, 100)}");

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
            }
        }
    }
}