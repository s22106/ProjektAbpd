using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjektAbpd.Client.Helper;
using ProjektAbpd.Shared.Models.DTOs;

namespace ProjektAbpd.Client.Services
{
    public class PriceService : IPriceService
    {
        private readonly HttpClient _httpClient;

        public PriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddStockPricesToDatabase(List<PriceDTO> stockPrices, string ticker)
        {
            string json = JsonConvert.SerializeObject(stockPrices);
            var uri = $"/api/stocks/{ticker}/Prices";
            await _httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<List<PriceDTO>> GetPricesForStock(string ticker)
        {
            var now = DateTime.Now;
            string uri = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{now.AddDays(-90).ToString("yyyy-MM-dd")}/{now.ToString("yyyy-MM-dd")}?adjusted=true&sort=desc&limit=90&apiKey=3hZzmHuqOO0DqsFAtAR8Uva4sLW1tsHu";

            string json = "";
            try
            {

                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                var result = JObject.Parse(json).SelectToken("results").ToObject<List<PriceDTO>>();
                for (int i = 0; i < result.Count; i++)
                {
                    result[result.Count - 1 - i].Date = DateTime.Now.AddDays(-result.Count + 1 + i);
                }
                return result;
            }
            catch (HttpRequestException)
            {
                uri = $"/api/stocks/{ticker}/Prices";
                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                var jarr = JArray.Parse(json);
                return jarr.ToObject<List<PriceDTO>>();
            }
        }
    }
}