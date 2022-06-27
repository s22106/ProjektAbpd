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
    public class ArticleService : IArticleService
    {
        private readonly HttpClient _httpClient;

        public ArticleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddArticlesToDatabase(List<ArticleDTO> articles, string ticker)
        {

            string json = JsonConvert.SerializeObject(articles);
            var uri = $"/api/articles/{ticker}";
            await _httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));

        }

        public async Task<List<ArticleDTO>> GetArticlesByTicker(string ticker)
        {
            string uri = $"https://api.polygon.io/v2/reference/news?ticker={ticker}&limit=5&apiKey=3hZzmHuqOO0DqsFAtAR8Uva4sLW1tsHu";
            string json = "";
            try
            {

                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                var result = JObject.Parse(json).SelectToken("results").ToObject<List<ArticleDTO>>();
                return result;
            }
            catch (HttpRequestException)
            {
                uri = $"/api/articles/{ticker}";
                json = await HttpConverter.ConvertHttpMessage(await _httpClient.GetAsync(uri));
                System.Console.WriteLine(json + "<--------jsonnnnnn");
                if (json == "") return new List<ArticleDTO>();
                var jarr = JArray.Parse(json);
                return jarr.ToObject<List<ArticleDTO>>();
            }
        }
    }
}