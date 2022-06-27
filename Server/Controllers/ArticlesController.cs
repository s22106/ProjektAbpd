using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjektAbpd.Server.Services;
using ProjektAbpd.Shared.Models.DTOs;
using Server.Models;
using Server.Services;
//using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IDbStockService _stockService;
        private readonly IDbArticleService _articleService;
        private readonly IDbSharedService _sharedService;
        public ArticlesController(IDbStockService stockService, IDbArticleService articleService, IDbSharedService sharedService)
        {
            _stockService = stockService;
            _articleService = articleService;
            _sharedService = sharedService;
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetArticlesForStock(string ticker)
        {
            if (!await _sharedService.CheckIfStockExists(ticker)) return NotFound("Stock does not exist in database");

            var stockId = await _stockService.GetStockIdByTicker(ticker);

            if (!await _articleService.CheckIfArticlesExist(stockId)) return NotFound("Stock does not have any saved articles");

            var body = await _articleService.GetArticlesById(stockId);


            return new JsonResult(body, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        [HttpPost("{ticker}")]
        public async Task<IActionResult> AddArticles(List<ArticleDTO> articles, string ticker)
        {
            var stockId = await _stockService.GetStockIdByTicker(ticker);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var article in articles)
                    {
                        if (!await _articleService.CheckIfArticleExists(article.article_url))
                        {
                            await _sharedService.CreateAsync(new DbArticle
                            {
                                Title = article.title,
                                ImageUrl = article.image_url,
                                ArticleUrl = article.article_url,
                                Description = article.description
                            });
                        }
                        if (!await _articleService.CheckIfStockArticleExists(article.article_url, stockId))
                        {
                            await _sharedService.CreateAsync(new DbStockArticle
                            {
                                StockId = stockId,
                                ArticleUrl = article.article_url
                            });
                        }
                    }
                    await _sharedService.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (Exception)
                {
                    return Problem("Server error");
                }
            }
            return NoContent();
        }
    }
}