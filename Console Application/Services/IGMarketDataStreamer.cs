using System;
using System.Threading;
using Lightstreamer.DotNet.Client;
using com.lightstreamer.client;
using System.Collections.Generic;
using IgBotTraderCLI.Models;

namespace IgBotTraderCLI.Services
{
    class IGMarketDataStreamer
    {
        public delegate void LightstreamerUpdateDelegate(int item, ItemUpdate values);
        public delegate void LightstreamerStatusChangedDelegate(int cStatus, string status);

        private LightstreamerUpdateDelegate updateDelegate;
        private LightstreamerStatusChangedDelegate statusChangeDelegate;

        private LightstreamerClient client;
        private Subscription subscription;

        public enum ListenerStatus
        {
            VOID = -1,
            DISCONNECTED = 0,
            POLLING = 1,
            STREAMING = 2,
            STALLED = 3
        }

        public IGMarketDataStreamer() { }

        public IGMarketDataStreamer(AccountDetails accountDetails)
        {
            CreateClient(accountDetails);
        }

        public void CreateClient(AccountDetails accountDetails)
        {


            client = new LightstreamerClient(accountDetails.LightstreamerEndpoint, "DEMO");
            CreateSubscription("IX.D.OMX.IFM.IP");

        }

        public void CreateSubscription(string market)
        {
            subscription = new Subscription("MERGE");

            subscription.Items = new string[1] { $"MARKET:{market}" };
            subscription.Fields = new string[5] { "UPDATE_TIME", "BID", "OFFER", "CHANGE_PCT", "MARKET_STATE" };
            subscription.DataAdapter = "QUOTE_ADAPTER";

            subscription.addListener(new MarketListener());

            client.subscribe(subscription);
        }
    }

    class MarketListener : SubscriptionListener
    {
        public void onClearSnapshot(string itemName, int itemPos)
        {
            Console.WriteLine("Clear Snapshot for " + itemName + ".");
        }

        public void onCommandSecondLevelItemLostUpdates(int lostUpdates, string key)
        {
            Console.WriteLine("Lost Updates for " + key + " (" + lostUpdates + ").");
        }

        public void onCommandSecondLevelSubscriptionError(int code, string message, string key)
        {
            Console.WriteLine("Subscription Error for " + key + ": " + message);
        }

        public void onEndOfSnapshot(string itemName, int itemPos)
        {
            Console.WriteLine("End of Snapshot for " + itemName + ".");
        }

        public void onItemLostUpdates(string itemName, int itemPos, int lostUpdates)
        {
            Console.WriteLine("Lost Updates for " + itemName + " (" + lostUpdates + ").");
        }

        public void onItemUpdate(ItemUpdate itemUpdate)
        {
            Console.WriteLine("New update for " + itemUpdate.ItemName);

            IDictionary<string, string> listc = itemUpdate.ChangedFields;
            foreach (string value in listc.Values)
            {
                Console.WriteLine(" >>>>>>>>>>>>> " + value);
            }
        }

        public void onListenEnd(Subscription subscription)
        {
            //throw new NotImplementedException();
        }

        public void onListenStart(Subscription subscription)
        {
            //throw new NotImplementedException();
        }

        public void onRealMaxFrequency(string frequency)
        {
            Console.WriteLine("Real frequency: " + frequency + ".");
        }

        public void onSubscription()
        {
            Console.WriteLine("Start subscription.");
        }

        public void onSubscriptionError(int code, string message)
        {
            Console.WriteLine("Subscription error: " + message);
        }

        public void onUnsubscription()
        {
            Console.WriteLine("Stop subscription.");
        }
    }
}
