using IgBotTraderCLI.Strategies;
using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IgBotTraderCLI.Listeners
{
    public class MarketDataListener : IHandyTableListener
    {
        private List<Strategy> Strategies { get; set; } = new List<Strategy>();
        public Dictionary<DateTime, List<decimal>> Period15 = new Dictionary<DateTime, List<decimal>>();

        public MarketDataListener()
        {
        }

        public MarketDataListener(List<Strategy> strategies) => Strategies = strategies;

        public MarketDataListener(Strategy strategy) => Strategies.Add(strategy);

        public void UpdatePeriod(decimal value, Dictionary<DateTime, List<decimal>> period, decimal minuteInterval)
        {
            if (period == null)
                return;

            var _d = DateTime.Now;
            DateTime date = new DateTime(_d.Year, _d.Month, _d.Day, _d.Hour,
                Convert.ToInt32(Math.Floor(_d.Minute / minuteInterval)), _d.Second);

            if (period.Count == 0)
                period.Add(date, new List<decimal>() { value });

            if (period.ContainsKey(date))
                period[date].Add(value);
            else
                period.Add(date, new List<decimal>() { value });
        }

        #region IHandyTableListener
        public void OnUpdate(int itemPos, string itemName, IUpdateInfo update)
        {
            decimal mid;
            try
            {
                mid = (decimal.Parse(update.GetNewValue("BID"), new CultureInfo("en-US"))
                    + decimal.Parse(update.GetNewValue("OFFER"), new CultureInfo("en-US"))) / 2m;
            }
            catch (Exception)
            {
                return;
            }
            
            Console.WriteLine($"{update.GetNewValue("UPDATE_TIME")} - {itemName} >> {update.GetNewValue("OFFER")} | {update.GetNewValue("BID")} | {mid.ToString(new CultureInfo("en-US"))}");

            UpdatePeriod(decimal.Parse(update.GetNewValue("BID"), new CultureInfo("en-US")), Period15, 15);

            foreach (var strategy in Strategies)
                strategy.UpdateData(itemPos, itemName, update);
        }

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
        #endregion
    }
}