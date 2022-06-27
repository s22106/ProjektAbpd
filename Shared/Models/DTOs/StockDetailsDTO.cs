using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektAbpd.Shared.Models.DTOs
{
    public class StockDetailsDTO
    {
#nullable enable
        //public Branding? branding { get; set; }
        public string? homepage_url { get; set; }
        public string? phone_number { get; set; }
        public string? primary_exchange { get; set; }
        public string? sic_description { get; set; }
        public Branding? branding { get; set; }
#nullable disable
        [Required]
        public string name { get; set; }
        [Required]
        public string ticker { get; set; }
    }

    public class Branding
    {
#nullable enable
        public string? icon_url { get; set; }
        public string? logo_url { get; set; }
#nullable disable
    }
}