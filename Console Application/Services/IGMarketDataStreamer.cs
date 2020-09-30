using System;
using System.Threading;
using Lightstreamer.DotNet.Client;
using com.lightstreamer.client;
using System.Collections.Generic;
using IgBotTraderCLI.Models;
using Console_Application.Strategies;
using Console_Application.Listeners;

namespace IgBotTraderCLI.Services
{
    class IGMarketDataStreamer
    {
        private LSClient Client { get; set; }

        public IGMarketDataStreamer() { }

        public IGMarketDataStreamer(AccountDetails accountDetails, IGApiAccount account)
        {
            Client = new LSClient();
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.User = accountDetails.Accounts[0].AccountId;
            connectionInfo.Password = $"CST-{account.CST}|XST-{account.XSecurityToken}";
            connectionInfo.PushServerUrl = accountDetails.LightstreamerEndpoint;

            Client.OpenConnection(connectionInfo, new ClientConnectionEvents());

            CreateSubscription("IX.D.OMX.IFM.IP");
        }

        public void CloseConnection()
        {
            Client.CloseConnection();
        }

        public void CreateSubscription(string market)
        {
            ExtendedTableInfo info = new ExtendedTableInfo(
                new string[1] { $"MARKET:{market}" }, "MERGE",
                new string[5] { "UPDATE_TIME", "BID", "OFFER", "CHANGE_PCT", "MARKET_STATE" }, true);

            Client.SubscribeItems(info, new MarketDataListener(new DebugStrategy()));
        }
    }

    class ClientConnectionEvents : IConnectionListener
    {
        public void OnActivityWarning(bool warningOn)
        {
            Console.WriteLine($"OnActivityWarning << Warning: {warningOn}");
        }

        public void OnClose()
        {
            Console.WriteLine("OnClose << Connection Closed");
        }

        public void OnConnectionEstablished()
        {
            Console.WriteLine("OnConnectionEstablished << Connection Established");
        }

        public void OnDataError(PushServerException e)
        {
            Console.WriteLine($"OnDataError << Push DataError: {e.Message}");
        }

        public void OnEnd(int cause)
        {
            Console.WriteLine($"OnEnd << {cause}");
        }

        public void OnFailure(PushServerException e)
        {
            Console.WriteLine($"OnFailure << Push Failure: {e.Message}");
        }

        public void OnFailure(PushConnException e)
        {
            Console.WriteLine($"OnFailure << Connection Failure: {e.Message}");
        }

        public void OnNewBytes(long bytes)
        {
            //Console.WriteLine($"OnNewBytes << {bytes}");
        }

        public void OnSessionStarted(bool isPolling)
        {
            Console.WriteLine($"OnSessionStarted << Polling: {isPolling}");
        }
    }
}
