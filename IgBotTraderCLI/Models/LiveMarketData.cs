using System;
using System.Collections.Generic;

namespace IgBotTraderCLI.Models
{
    public static class LiveMarketData
    {
        public static decimal Bid { get; set; }
        public static decimal Offer { get; set; }
        public static DateTime LastUpdate { get; set; }

        public static Dictionary<string, string> OpenPositions { get; set; }
    }
}