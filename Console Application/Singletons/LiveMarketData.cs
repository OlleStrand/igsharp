using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application.Singletons
{
    public static class LiveMarketData
    {
        static LiveMarketData() { }

        public static decimal Bid { get; set; }
        public static decimal Offer { get; set; }
        public static DateTime LastUpdate { get; set; }
    }
}
