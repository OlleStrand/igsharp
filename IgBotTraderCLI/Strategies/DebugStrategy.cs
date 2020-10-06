using IgBotTraderCLI.Services;
using IgBotTraderCLI.Models;
using Lightstreamer.DotNet.Client;
using System.Globalization;

namespace IgBotTraderCLI.Strategies
{
    public class DebugStrategy : Strategy
    {
        public override IGTradeService TradeService { get; set; }
        public override string StrategyName { get; protected set; }

        public DebugStrategy()
        {
            StrategyName = "DebugStrategy";
        }

        public override void UpdateData(int itemPos, string itemName, IUpdateInfo update)
        {
            LiveMarketData.Bid = decimal.Parse(update.GetNewValue("BID"), new CultureInfo("en-US"));
            LiveMarketData.Offer = decimal.Parse(update.GetNewValue("OFFER"), new CultureInfo("en-US"));
        }

        
    }
}