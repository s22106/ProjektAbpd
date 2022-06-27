using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjektAbpd.Client.Helper;
using Client.Helper;

namespace ProjektAbpd.Client.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly HttpClient _httpClient;

        public WatchlistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddToUserWatchlist(StockDetailsDTO stock)
        {
            string json = JsonConvert.SerializeObject(stock);
            var uri = $"/api/watchlist";
            await _httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        public async Task RemoveFromWatchlist(StockDetailsDTO stock)
        {
            string json = JsonConvert.SerializeObject(stock);
            var uri = $"/api/watchlist/{stock.ticker}";
            var msg = await _httpClient.DeleteAsync(uri);
        }

        public async Task<List<StockDetailsDTO>> RequestUserWatchlist()
        {
            try
            {
                var uri = $"/api/watchlist";
                string json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                return JArrayParser.Parse<StockDetailsDTO>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}