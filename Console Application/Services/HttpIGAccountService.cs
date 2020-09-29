using IgBotTraderCLI.Models;
using RestSharp;
using System.Net;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IgBotTraderCLI.Services
{
    class HttpIGAccountService
    {
        #region Public Properties
        public RestClient Client { get; set; }
        public IGApiAccount Account { get; set; }
        public AccountDetails AccountDetails { get; set; }
        public IGTradeService TradeService { get; set; }

        #endregion

        #region Private variables

        private bool _initilized = false;
        private const string URL = "https://demo-api.ig.com/gateway/deal";
        #endregion

        public HttpIGAccountService(string url = "https://demo-api.ig.com/gateway/deal")
        {
            try
            {
                Client = new RestClient(url);
                TradeService = new IGTradeService(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpIGAccountService(IGApiAccount account, string url = "https://demo-api.ig.com/gateway/deal")
        {
            try
            {
                Client = new RestClient(url);
                Account = account;

                Client.AddDefaultHeaders(new Dictionary<string, string>()
                {
                    {"Content-Type", "application/json; charset=UTF-8"},
                    {"Accept", "application/json; charset=UTF-8"},
                    {"X-IG-API-KEY", Account.ApiKey}
                });

                if (!_initilized)
                    AccountDetails = Authenticate(account);

                TradeService = new IGTradeService(this);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AccountDetails Authenticate(IGApiAccount apiAccount)
        {
            try
            {
                var request = new RestRequest("session", Method.POST)
                    .AddHeader("Version", "2")
                    .AddJsonBody(new { identifier = Account.Username, password = Account.Password, encryptedPassword = "" });

                var response = Client.Execute(request);

                Console.WriteLine(response.Content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    foreach (var item in response.Headers)
                    {
                        if (apiAccount.CST != "" && apiAccount.XSecurityToken != "")
                            break;

                        if (item.Name == "CST")
                            apiAccount.CST = item.Value.ToString();

                        if (item.Name == "X-SECURITY-TOKEN")
                            apiAccount.XSecurityToken = item.Value.ToString();
                    }

                    //Start reauth method
                    _initilized = true;
                    return JsonConvert.DeserializeObject<AccountDetails>(response.Content);
                }

                return null;
            }
            catch (Exception)
            {
                _initilized = false;
                return null;
            }
        }

        public void PlaceOrder()
        {

        }
    }
}
