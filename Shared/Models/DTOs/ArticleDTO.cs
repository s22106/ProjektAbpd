using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAbpd.Shared.Models.DTOs
{
    public class ArticleDTO
    {
        public string article_url { get; set; }
        public string image_url { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }
}