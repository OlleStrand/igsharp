using System;

using System.Threading;
using Lightstreamer.DotNet.Client;
using com.lightstreamer.client;
using System.Collections.Generic;

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
    }
}
