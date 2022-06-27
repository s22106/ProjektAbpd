using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAbpd.Shared.Models.DTOs
{
    public class PriceDTO
    {
        public double c { get; set; }
        public double h { get; set; }
        public double l { get; set; }
        public double o { get; set; }
        public int v { get; set; }
        public DateTime Date { get; set; }
    }
}