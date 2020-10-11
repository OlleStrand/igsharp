using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TANet.Contracts.Models;
using TANet.Core;

namespace IgBotTraderCLI.Services
{
    public class HistoricalDataService
    {
        private RestClient Client { get; set; }
        public Dictionary<DateTime, List<Candle>> HistoricalData { get; set; }

        public HistoricalDataService()
        {
            Client = new RestClient("https://yahoo-finance15.p.rapidapi.com/api/yahoo");
        }


        public void LoadHistoricalData()
        {

        }

    }
}
