using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Models
{
    class AccountDetails
    {
        public string AccountType { get; set; }
        public AccountInfo AccountInfo { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrentAccountId { get; set; }
        public string LightstreamerEndpoint { get; set; }
        public Account[] Accounts { get; set; }
        public int ClientId { get; set; }
        public byte TimezoneOffset { get; set; }
        public bool HasActiveDemoAccounts { get; set; }
        public bool HasActiveLiveAccounts { get; set; }
        public bool TrailingStopsEnabled { get; set; }
        public string ReroutingEnviroment { get; set; }
        public bool DealingEnabled { get; set; }
    }

    class AccountInfo
    {
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }
        public decimal ProfitLoss { get; set; }
        public decimal Available { get; set; }
    }

    class Account
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public bool Preferred { get; set; }
        public string AccountType { get; set; }
    }
}
