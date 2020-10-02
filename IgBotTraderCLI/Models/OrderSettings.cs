using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Application.Models
{
    class OrderSettings
    {
        public string Epic { get; set; }
        public DateTime Expiry { get; set; }
        public string Direction { get; set; }
        public int Size { get; set; }
        public string OrderType { get; set; }
        public TimeSpan TimeInForce { get; set; }
        public decimal Level { get; set; }
        public bool GuaranteedStop { get; set; }
        public decimal StopLevel { get; set; }
        public decimal StopDistance { get; set; }
        public bool TrailingStop { get; set; }
        public decimal TrailingStopincrement { get; set; }
        public bool ForceOpen { get; set; }
        public decimal LimitLevel { get; set; }
        public decimal LimitDistance { get; set; }
        public string QuoteId { get; set; }
        public string CurrencyCode { get; set; }
    }
}