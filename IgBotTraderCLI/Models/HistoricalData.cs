using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Models
{
    public class HistoricalData
    {
        [JsonProperty("meta")]
        public HistoricalDataMeta Meta { get; set; }

        [JsonProperty("items")]
        public Dictionary<string, OhlcData> Items { get; set; }

        public Dictionary<DateTime, OhlcData> ItemDictionary { get; set; }

        public void PopulateItemDictionary()
        {
            ItemDictionary = new Dictionary<DateTime, OhlcData>();
            foreach (KeyValuePair<string, OhlcData> entry in Items)
            {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(int.Parse(entry.Key)).ToLocalTime();

                ItemDictionary.Add(date, entry.Value);
            }
            Items = null;
            GC.Collect();
        }
    }

    public class HistoricalDataMeta
    {
        [JsonProperty("currency")]
        public string CurrencyCode { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("instrumentType")]
        public string InstrumentType { get; set; }

        [JsonProperty("firstTradeDate")]
        public string FirstTradeDateUnix { get; set; }

        public DateTime FirstTradeDate
        { 
            get { return UnixTimeStampToDateTime(int.Parse(FirstTradeDateUnix)); }
            set { FirstTradeDateUnix = ((int)value.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(); }
        }

        [JsonProperty("regularMarketTime")]
        public string RegularMarketTimeUnix { get; set; }

        public DateTime RegularMarketTime
        {
            get { return UnixTimeStampToDateTime(int.Parse(RegularMarketTimeUnix)); }
            set { RegularMarketTimeUnix = ((int)value.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(); }
        }

        [JsonProperty("gmtoffset")]
        public int Gmtoffset { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("regularMarketPrice")]
        public decimal RegularMarketPrice { get; set; }

        [JsonProperty("chartPreviousClose")]
        public decimal ChartPreviousClose { get; set; }

        [JsonProperty("previousClose")]
        public decimal PreviousClose { get; set; }

        [JsonProperty("scale")]
        public int Scale { get; set; }

        [JsonProperty("priceHint")]
        public int PriceHint { get; set; }

        [JsonProperty("dataGranularity")]
        public string DataGranularity { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }

    public class OhlcData
    {
        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }
    }
}
