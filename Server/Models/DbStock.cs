using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DbStock
    {
        public int StockId { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public string HomepageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Category { get; set; }
        public string PrimaryExchange { get; set; }
        public virtual IEnumerable<DbStockPrice> Prices { get; set; }
        public virtual IEnumerable<DbUserStocks> Watchlist { get; set; }
        public virtual IEnumerable<DbStockArticle> Articles { get; set; }
    }
}