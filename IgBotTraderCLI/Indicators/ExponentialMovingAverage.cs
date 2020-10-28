using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace IgBotTraderCLI.Indicators
{
    class ExponentialMovingAverage
    {
        private SimpleMovingAverage simpleMovingAverage;
        private decimal k;
        private int period;
        private decimal current;
        private decimal previousEma;

        public ExponentialMovingAverage(decimal[] input, int period)
        {
            this.period = period;
            current = input[input.Length - 1];
            previousEma = -1;
            simpleMovingAverage = new SimpleMovingAverage(input, period);
            CalculateK();
        }

        public ExponentialMovingAverage(int period, ExponentialMovingAverage ema)
        {
            this.period = period;
            previousEma = ema.Ema();
            CalculateK();
        }

        public ExponentialMovingAverage(int period, decimal previousEma)
        {
            this.period = period;
            this.previousEma = previousEma;
            CalculateK();
        }

        private void CalculateK() => k = 2 / (period + 1);

        public decimal Ema()
        {
            if (previousEma <= 0 && simpleMovingAverage != null)
                return k * (current - simpleMovingAverage.Sma()) + simpleMovingAverage.Sma();

            return k * (current - previousEma) + previousEma;
        }
    }
}
