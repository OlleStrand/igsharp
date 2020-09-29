using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Models
{
    class IGApiAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string CST { get; set; }
        public string XSecurityToken { get; set; }

        public IGApiAccount() { }

        public IGApiAccount(string username, string password, string apikey)
        {
            Username = username;
            Password = password;
            ApiKey = apikey;
        }
    }
}
