using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinanceScraper.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string PChange { get; set; }
        public string Currency { get; set; }
        [Display(Name = "MarketTime"), DataType(DataType.Date)]
        public DateTime MarketTime { get; set; }
        public string Volume { get; set; }
        public string AVolume { get; set; }
        public string MarketCap { get; set; }
    }
}
