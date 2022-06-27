using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models;
using ProjektAbpd.Shared.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjektAbpd.Client.Helper;

namespace ProjektAbpd.Client.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;

        public StockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddStockToDatabase(StockDetailsDTO stock)
        {
            string json = JsonConvert.SerializeObject(stock);
            System.Console.WriteLine(stock.ticker + "--------------------------");
            var uri = $"/api/stocks";
            await _httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task<StockDetailsDTO> GetStockDetailsByTicker(string ticker)
        {
            string uri = $"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey=3hZzmHuqOO0DqsFAtAR8Uva4sLW1tsHu";
            string json = "";
            try
            {
                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                return JObject.Parse(json).SelectToken("results").ToObject<StockDetailsDTO>();
            }
            catch (HttpRequestException)
            {
                uri = $"/api/stocks/{ticker}";
                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                return JObject.Parse(json).ToObject<StockDetailsDTO>();
            }
        }

        public async Task<List<Stock>> GetStocksListByTicker(string ticker)
        {
            string uri = $"https://api.polygon.io/v3/reference/tickers?market=stocks&search={ticker}&active=true&sort=ticker&order=asc&limit=10&apiKey=3hZzmHuqOO0DqsFAtAR8Uva4sLW1tsHu";
            string json = "";
            try
            {
                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                return JObject.Parse(json).SelectToken("results").ToObject<List<Stock>>();
            }
            catch (HttpRequestException)
            {
                uri = $"/api/stocks/{ticker}/all";
                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                var jarr = JArray.Parse(json);
                return jarr.ToObject<List<Stock>>();
            }
        }
    }
}