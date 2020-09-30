using Console_Application.Singletons;
using IgBotTraderCLI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Services
{
    class IGTradeService
    {
        private HttpIGAccountService IGAccountService { get; set; }
        private RestClient Client { get; set; }

        public IGTradeService(HttpIGAccountService iGAccountService)
        {
            IGAccountService = iGAccountService;
            Client = new RestClient("https://api.exchangeratesapi.io");
        }

        public decimal GetExchangeRate(string from, string to)
        {
            try
            {
                var request = new RestRequest("latest", Method.GET);

                var response = Client.Execute(request);

                RatesResponse rateRes = JsonConvert.DeserializeObject<RatesResponse>(response.Content);

                return rateRes.Rates[to] / rateRes.Rates[from];
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CalculateBuyOrderSize(string marketIsoCode, decimal accountUsage = 1m, int maxOrder = 391)
        {
            if (accountUsage > 1m)
                return 0;

            if (LiveMarketData.Bid == 0)
                return 0;

            // (20 * pris * 0,75%) / account = ordersize

            decimal balance = IGAccountService.AccountDetails.AccountInfo.Balance * GetExchangeRate(IGAccountService.AccountDetails.CurrencyIsoCode, marketIsoCode);
            decimal margin = 20 * LiveMarketData.Bid * 0.0075m;

            decimal count = (balance / margin) * accountUsage;

            if (count < maxOrder)
                return Convert.ToInt32(Math.Floor(count));

            return maxOrder;
        }

        public int CalculateSellOrderSize(string marketIsoCode, decimal accountUsage = 1m, int maxOrder = 391)
        {
            if (accountUsage > 1m)
                return 0;

            if (LiveMarketData.Offer == 0)
                return 0;

            // (20 * pris * 0,75%) / account = ordersize

            decimal balance = IGAccountService.AccountDetails.AccountInfo.Balance * GetExchangeRate(IGAccountService.AccountDetails.CurrencyIsoCode, marketIsoCode);
            decimal margin = 20 * LiveMarketData.Offer * 0.0075m;

            decimal count = (balance / margin) * accountUsage;

            if (count < maxOrder)
                return Convert.ToInt32(Math.Floor(count));

            return maxOrder;
        }
    } 

    class RatesResponse
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
