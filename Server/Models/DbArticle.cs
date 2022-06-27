using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DbArticle
    {
        public string ArticleUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual IEnumerable<DbStockArticle> StockArticles { get; set; }
    }
}