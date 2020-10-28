using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgBotTraderCLI.Models
{
    public class IGHistoricalData
    {
        [JsonProperty("prices")]
        public List<Price> Prices { get; set; }

        [JsonProperty("instrumentType")]
        public string InstrumentType { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class Price
    {
        [JsonProperty("snapshotTime")]
        public string SnapshotTime { get; set; }

        [JsonProperty("snapshotTimeUTC")]
        public DateTime SnapshotTimeUTC { get; set; }

        [JsonProperty("openPrice")]
        public OpenPrice OpenPrice { get; set; }

        [JsonProperty("closePrice")]
        public ClosePrice ClosePrice { get; set; }

        [JsonProperty("highPrice")]
        public HighPrice HighPrice { get; set; }

        [JsonProperty("lowPrice")]
        public LowPrice LowPrice { get; set; }

        [JsonProperty("lastTradedVolume")]
        public int LastTradedVolume { get; set; }

        public void FixSnapshotTimeUTC()
        {
            try
            {
                //2020:10:20-08:30:00
                string[] splitTime = SnapshotTime.Split(':');

                SnapshotTimeUTC = DateTime.ParseExact($"{splitTime[0]}/{splitTime[1]}/{splitTime[2]}:{splitTime[3]}:{splitTime[4]}",
                    "yyyy/MM/dd-hh:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception) { }
        }
    }

    public class OpenPrice
    {
        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("lastTraded")]
        public object LastTraded { get; set; }
    }

    public class ClosePrice
    {
        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("lastTraded")]
        public object LastTraded { get; set; }
    }

    public class HighPrice
    {
        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("lastTraded")]
        public object LastTraded { get; set; }
    }

    public class LowPrice
    {
        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("lastTraded")]
        public object LastTraded { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("allowance")]
        public Allowance Allowance { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("pageData")]
        public PageData PageData { get; set; }
    }

    public class Allowance
    {
        [JsonProperty("remainingAllowance")]
        public int RemainingAllowance { get; set; }

        [JsonProperty("totalAllowance")]
        public int TotalAllowance { get; set; }

        [JsonProperty("allowanceExpiry")]
        public int AllowanceExpiry { get; set; }
    }

    public class PageData
    {
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
    }
}
