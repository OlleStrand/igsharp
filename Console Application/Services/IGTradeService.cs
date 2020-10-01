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

        public IGTradeService()
        {
            Client = new RestClient("https://api.exchangeratesapi.io");
        }

        public void SetAccountServiceToMain()
        {
            IGAccountService = HttpIGAccountService.MainIGService;
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

        public int CalculateBuyOrderSize(string marketIsoCode, decimal marginPercentage, decimal accountUsage = 1m, int maxOrder = 391)
        {
            //Must be percentage in decimal form
            if (accountUsage > 1m)
                return 0;

            //Safety check. Cannot divide with 0
            if (LiveMarketData.Bid == 0)
                return 0;

            decimal balance = IGAccountService.AccountDetails.AccountInfo.Balance * GetExchangeRate(IGAccountService.AccountDetails.CurrencyIsoCode, marketIsoCode);
            decimal margin = 20 * LiveMarketData.Bid * marginPercentage;

            decimal orderSize = (balance / margin) * accountUsage;

            if (orderSize < maxOrder)
                return Convert.ToInt32(Math.Floor(orderSize));

            return maxOrder;
        }

        public int CalculateSellOrderSize(string marketIsoCode, decimal marginPercentage, decimal accountUsage = 1m, int maxOrder = 391)
        {
            if (accountUsage > 1m)
                return 0;

            if (LiveMarketData.Offer == 0)
                return 0;

            decimal balance = IGAccountService.AccountDetails.AccountInfo.Balance * GetExchangeRate(IGAccountService.AccountDetails.CurrencyIsoCode, marketIsoCode);
            decimal margin = 20 * LiveMarketData.Offer * marginPercentage;

            decimal orderSize = (balance / margin) * accountUsage;

            if (orderSize < maxOrder)
                return Convert.ToInt32(Math.Floor(orderSize));

            return maxOrder;
        }
    } 

    class RatesResponse
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
