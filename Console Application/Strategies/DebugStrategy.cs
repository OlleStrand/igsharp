using IgBotTraderCLI.Services;
using IgBotTraderCLI.Singletons;
using Lightstreamer.DotNet.Client;
using System.Globalization;

namespace IgBotTraderCLI.Strategies
{
    internal class DebugStrategy : IStrategy
    {
        public IGTradeService TradeService { get; set; }

        public void UpdateData(int itemPos, string itemName, IUpdateInfo update)
        {
            LiveMarketData.Bid = decimal.Parse(update.GetNewValue("BID"), new CultureInfo("en-US"));
            LiveMarketData.Offer = decimal.Parse(update.GetNewValue("OFFER"), new CultureInfo("en-US"));
        }
    }
}