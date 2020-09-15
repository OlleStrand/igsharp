using Console_Application.Models;
using RestSharp;
using System.Net;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application.Services
{
    class HttpIGAccountService
    {
        #region Public Properties

        public RestClient Client { get; set; }

        #endregion

        #region Private variables

        private bool _initilized = false;

        #endregion

        public HttpIGAccountService(string url = "https://demo-api.ig.com/gateway/deal")
        {
            try
            {
                Client = new RestClient(url);
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

                if (Authenticate(account).StatusCode == HttpStatusCode.OK)
                {
                    _initilized = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IRestResponse Authenticate(IGApiAccount account)
        {
            if (_initilized) return null;

            var request = new RestRequest("session", Method.POST);

            request.AddJsonBody(new { identifier = account.Username, password = account.Password, encryptedPassword = "" });
            request.AddHeader("X-IG-API-KEY", account.ApiKey);
            request.AddHeader("Version", "2");
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            request.AddHeader("Accept", "application/json; charset=UTF-8");

            var response = Client.Post(request);

            Console.WriteLine(response.Content);

            return response;
        }
    }
}
