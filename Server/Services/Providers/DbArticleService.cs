using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektAbpd.Shared.Models.DTOs;
using ProjektAbpd.Server.Data;

namespace Server.Services.Providers
{
    public class DbArticleService : IDbArticleService
    {
        private readonly ApplicationDbContext _context;

        public DbArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfArticleExists(string article_url)
        {
            return await _context.Articles.AnyAsync(e => e.ArticleUrl == article_url);
        }

        public Task<bool> CheckIfArticlesExist(int stockId)
        {
            return _context.StockArticles.AnyAsync(e => e.StockId == stockId);
        }

        public async Task<bool> CheckIfStockArticleExists(string article_url, int stockId)
        {
            return await _context.StockArticles.AnyAsync(e => e.ArticleUrl == article_url && e.StockId == stockId);
        }

        public async Task<List<ArticleDTO>> GetArticlesById(int stockId)
        {
            return await _context.StockArticles.Where(e => e.StockId == stockId)
            .Include(e => e.Article)
            .Select(a => new ArticleDTO
            {
                title = a.Article.Title,
                description = a.Article.Description,
                image_url = a.Article.ImageUrl,
                article_url = a.ArticleUrl
            }).Take(5)
            .ToListAsync();
        }
    }
}