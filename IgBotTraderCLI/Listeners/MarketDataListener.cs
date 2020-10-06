using IgBotTraderCLI.Strategies;
using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IgBotTraderCLI.Listeners
{
    internal class MarketDataListener : IHandyTableListener
    {
        private List<Strategy> Strategies { get; set; } = new List<Strategy>();

        public MarketDataListener()
        {
        }

        public MarketDataListener(List<Strategy> strategies) => Strategies = strategies;

        public MarketDataListener(Strategy strategy) => Strategies.Add(strategy);

        public void OnRawUpdatesLost(int itemPos, string itemName, int lostUpdates)
        {
            Console.WriteLine($"OnRawUpdatesLost >> {itemPos} - {itemName} - {lostUpdates}");
        }

        public void OnSnapshotEnd(int itemPos, string itemName)
        {
            Console.WriteLine($"OnSnapshotEnd >> {itemPos} - {itemName}");
        }

        public void OnUnsubscr(int itemPos, string itemName)
        {
            Console.WriteLine($"OnUnsubscr >> {itemPos} - {itemName}");
        }

        public void OnUnsubscrAll()
        {
            Console.WriteLine($"OnUnsubscrAll");
        }

        public void OnUpdate(int itemPos, string itemName, IUpdateInfo update)
        {
            decimal mid = (decimal.Parse(update.GetNewValue("BID"), new CultureInfo("en-US")) + decimal.Parse(update.GetNewValue("OFFER"), new CultureInfo("en-US"))) / 2m;

            Console.WriteLine($"{update.GetNewValue("UPDATE_TIME")} - {itemName} >> {update.GetNewValue("OFFER")} | {update.GetNewValue("BID")} | {mid.ToString(new CultureInfo("en-US"))}");

            foreach (var strategy in Strategies)
                strategy.UpdateData(itemPos, itemName, update);
        }
    }
}