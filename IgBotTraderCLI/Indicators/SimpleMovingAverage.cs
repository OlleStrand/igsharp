using Akka.Routing;
using IgBotTraderCLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Indicators
{
    public class SimpleMovingAverage
    {
        private Queue<decimal> Dataset = new Queue<decimal>();
        private readonly int period;
        private decimal sum;

        public SimpleMovingAverage(int period) => this.period = period;

        public SimpleMovingAverage(decimal[] input, int period)
        {
            this.period = period;

            for (int i = 0; i < input.Length; i++)
            {
                AddData(input[i]);
            }
        }

        public void AddData(decimal num)
        {
            sum += num;
            Dataset.Enqueue(num);

            if (Dataset.Count > period)
                sum -= Dataset.Dequeue();
        }

        public decimal Sma() => sum / period;
    }
}
