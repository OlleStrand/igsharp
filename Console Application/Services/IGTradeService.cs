using Console_Application.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application.Services
{
    class IGTradeService
    {
        private HttpIGAccountService IGAccountService { get; set; }
        private RestClient Client { get; set; }

        private const decimal MARGIN_PERCENT = 0.15m;

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

        public int CalculateOrderSize(decimal accountUsage = 1m)
        {
            if (accountUsage > 1m)
                return 0;

            return Convert.ToInt32(Math.Floor((IGAccountService.AccountDetails.AccountInfo.Balance * GetExchangeRate("CHF", "SEK")) / (1791 * MARGIN_PERCENT) * accountUsage));
        }
    } 

    class RatesResponse
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
