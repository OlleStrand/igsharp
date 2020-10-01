using Console_Application.Singletons;
using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Console_Application.Listeners
{
    class MarketDataListener : IHandyTableListener
    {
        private List<IStrategy> Strategies { get; set; } = new List<IStrategy>();

        public MarketDataListener() { }
        public MarketDataListener(List<IStrategy> strategies) => Strategies = strategies;
        public MarketDataListener(IStrategy strategy) => Strategies.Add(strategy);

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

            Console.WriteLine($"{update.GetNewValue("UPDATE_TIME")} - {itemName} >> {update.GetNewValue("OFFER")} | {update.GetNewValue("BID")} | {mid}");

            foreach (var strategy in Strategies)
                strategy.UpdateData(itemPos, itemName, update);
        }
    }
}
