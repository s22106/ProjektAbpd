using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Server.Models;

namespace Server.Models
{
    public class DbUserStocks
    {
        public string UserId { get; set; }
        public int StockId { get; set; }
        public virtual DbStock Stock { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}