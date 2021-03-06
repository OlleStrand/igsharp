﻿using IgBotTraderCLI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace IgBotTraderCLI.Services
{
    public class HttpIGAccountService
    {
        #region Public Properties

        public static RestClient Client
        {
            get
            {
                if (client == null)
                {
                    lock (clientLock)
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

        #endregion Public Properties

        #region Private variables

        private bool _initilized = false;
        private const string URL = "https://demo-api.ig.com/gateway/deal";

        private static RestClient client = null;
        private static HttpIGAccountService mainService = null;
        private static readonly object clientLock = new object();
        private static readonly object serviceLock = new object();

        #endregion Private variables

        public HttpIGAccountService()
        {
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
            try
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
            }
            catch (Exception)
            {
                throw;
            }
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

                    Client.AddDefaultHeaders(new Dictionary<string, string>()
                    {
                        {"X-SECURITY-TOKEN", Account.XSecurityToken},
                        {"CST", Account.CST}
                    });

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

        public string PlaceOrder(string epic, string direction, int size, string currency)
        {
            try
            {
                var request = new RestRequest("positions/otc", Method.POST)
                    .AddHeader("Version", "2")
                    .AddJsonBody(new PositionOrder(epic, direction, size, currency));

                var response = Client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<DealPlaced>(response.Content).DealReference;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}