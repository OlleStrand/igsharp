using IgBotTraderCLI.Models;
using IgBotTraderCLI.Services;
using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Strategies
{
    public abstract class Strategy
    {
        public virtual IGTradeService TradeService { get; set; }
        public abstract string StrategyName { get; protected set; }
        public abstract void UpdateData(int itemPos, string itemName, IUpdateInfo update);

        public virtual void OpenPosition(string epic, string direction, int size, string currency)
        {
            if (TradeService == null) return;

            try
            {
                string orderId = TradeService.PlaceOrder(epic, direction, size, currency);

                LiveMarketData.OpenPositions.Add(StrategyName, orderId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
