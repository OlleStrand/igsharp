using System;

namespace IgBotTraderCLI.Singletons
{
    public static class LiveMarketData
    {
        static LiveMarketData()
        {
        }

        public static decimal Bid { get; set; }
        public static decimal Offer { get; set; }
        public static DateTime LastUpdate { get; set; }
    }
}