using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DbStockPrice
    {
        public int StockId { get; set; }
        public DateTime Date { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public int Volume { get; set; }
        public virtual DbStock Stock { get; set; }
    }
}