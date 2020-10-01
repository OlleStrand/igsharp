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
        public static RestClient Client
        {
            get
            {
                if (client == null)
                {
                    lock(clientLock)
                    {
                        if (client == null)
                            client = new RestClient(URL);
                    }
                }
                return client;
            }
        }
        public static HttpIGAccountService MainIGService
        {
            get
            {
                if (mainService == null)
                {
                    lock (serviceLock)
                    {
                        if (mainService == null)
                            mainService = new HttpIGAccountService();
                    }
                }
                return mainService;
            }
        }
        public IGApiAccount Account { get; set; }
        public AccountDetails AccountDetails { get; set; }
        public IGTradeService TradeService { get; set; }
        #endregion

        #region Private variables
        private bool _initilized = false;
        private const string URL = "https://demo-api.ig.com/gateway/deal";

        private static RestClient client = null;
        private static HttpIGAccountService mainService = null;
        private static readonly object clientLock = new object();
        private static readonly object serviceLock = new object();
        #endregion

        public HttpIGAccountService()
        {
            try
            {
                TradeService = new IGTradeService();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HttpIGAccountService(IGApiAccount account, bool canBeMainService = false)
        {
            try
            {
                Initialize(account, canBeMainService);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Initialize(IGApiAccount account, bool canBeMainService = false)
        {
            Account = account;

            Client.AddDefaultHeaders(new Dictionary<string, string>()
            {
                {"Content-Type", "application/json; charset=UTF-8"},
                {"Accept", "application/json; charset=UTF-8"},
                {"X-IG-API-KEY", Account.ApiKey}
            });

            if (!_initilized)
            {
                AccountDetails = Authenticate();
                if (mainService == null && canBeMainService)
                    lock (serviceLock)
                        mainService = this;
            }

            TradeService = new IGTradeService();
        }

        public void UpdateAccount(IGApiAccount account) => Account = account;

        public AccountDetails Authenticate()
        {
            try
            {
                var request = new RestRequest("session", Method.POST)
                    .AddHeader("Version", "2")
                    .AddJsonBody(new { identifier = Account.Username, password = Account.Password, encryptedPassword = "" });

                var response = Client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    foreach (var item in response.Headers)
                    {
                        if (Account.CST != null && Account.XSecurityToken != null)
                            break;

                        if (item.Name == "CST")
                            Account.CST = item.Value.ToString();

                        if (item.Name == "X-SECURITY-TOKEN")
                            Account.XSecurityToken = item.Value.ToString();
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
