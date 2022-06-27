

namespace ProjektAbpd.Shared.Models
{
    public class Stock
    {
        //[JsonProperty("currency_name")]
        public string currency_name { get; set; }
        public string locale { get; set; }
        public string name { get; set; }
        public string primary_exchange { get; set; }
        public string ticker { get; set; }
        public string type { get; set; }

    }
}