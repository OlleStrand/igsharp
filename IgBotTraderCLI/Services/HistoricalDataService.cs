using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using IgBotTraderCLI.Models;

namespace IgBotTraderCLI.Services
{
    public class HistoricalDataService
    {
        private RestClient Client { get; set; }
        public Dictionary<DateTime, OhlcData> HistoricalDataYahoo { get; set; }
        public IGHistoricalData HistoricalDataIG { get; set; }

        public HistoricalDataService()
        {
            Client = new RestClient("https://yahoo-finance15.p.rapidapi.com/api/yahoo");
            Client.AddDefaultHeaders(new Dictionary<string, string>()
            {
                { "x-rapidapi-host", "yahoo-finance15.p.rapidapi.com" },
                { "x-rapidapi-key", "aeabafe2c8msh97867dc0d3cc332p128d50jsn94882f7d936a" },
                { "useQueryString", "true" }
            });
        }

        public decimal[] OhlcListToArray(int size, IGHistoricalData data)
        {
            if (size > data.Prices.Count)
                size = data.Prices.Count - 1;

            decimal[] vs = new decimal[size];

            for (int i = 0; i < vs.Length; i++)
            {
                vs[i] = Convert.ToDecimal(data.Prices[i].ClosePrice.Ask);
            }

            return vs;
        }

        public void LoadIGHistoricalData(string epic, string resolution, int pricePoints)
        {
            try
            {
                var request = new RestRequest($"prices/{epic}/{resolution}/{pricePoints}", Method.GET);

                var response = HttpIGAccountService.Client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    HistoricalDataIG = JsonConvert.DeserializeObject<IGHistoricalData>(response.Content);
            }
            catch (Exception) { }
        }

        public void LoadHistoricalData(string symbol, string interval)
        {
            try
            {
                var request = new RestRequest($"hi/history/{symbol}/{interval}", Method.GET);

                var response = Client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HistoricalData res = JsonConvert.DeserializeObject<HistoricalData>(response.Content);

                    res.PopulateItemDictionary();

                    HistoricalDataYahoo = res.ItemDictionary;
                }               
            }
            catch (Exception) { }
        }

    }
}
