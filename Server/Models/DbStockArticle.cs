using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DbStockArticle
    {
        public int StockId { get; set; }
        public string ArticleUrl { get; set; }
        public virtual DbStock Stock { get; set; }
        public virtual DbArticle Article { get; set; }
    }
}