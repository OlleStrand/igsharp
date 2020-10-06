using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Models
{
    public class PositionOrder
    {
        public string epic { get; set; }
        public string expiry { get; set; }
        public string direction { get; set; }
        public int size { get; set; }
        public string orderType { get; set; }
        public string timeInForce { get; set; }
        public bool guaranteedStop { get; set; }
        public bool forceOpen { get; set; }
        public string currencyCode { get; set; }

        public PositionOrder() { }

        public PositionOrder(string epic, string direction, int size, string currency)
        {
            DefaultPurchase(epic, direction, size, currency);
        }

        public void DefaultPurchase(string epic, string direction, int size, string currency)
        {
            this.epic = epic;
            expiry = "-";
            this.direction = direction;
            this.size = size;
            orderType = "MARKET";
            timeInForce = "FILL_OR_KILL";
            guaranteedStop = false;
            forceOpen = true;
            currencyCode = currency;
        }
    }

    public class DealPlaced
    {
        public string DealReference { get; set; }
    }
}
