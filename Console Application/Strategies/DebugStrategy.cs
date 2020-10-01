using Console_Application.Singletons;
using IgBotTraderCLI.Services;
using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application.Strategies
{
    class DebugStrategy : IStrategy
    {
        public IGTradeService TradeService { get; set; }

        public void UpdateData(int itemPos, string itemName, IUpdateInfo update)
        {
            LiveMarketData.Bid = decimal.Parse(update.GetNewValue("BID"), new CultureInfo("en-US"));
            LiveMarketData.Offer = decimal.Parse(update.GetNewValue("OFFER"), new CultureInfo("en-US"));
        }
    }
}
