using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;

namespace Server.Services
{
    public interface IDbArticleService
    {
        public Task<bool> CheckIfArticlesExist(int stockId);
        public Task<List<ArticleDTO>> GetArticlesById(int stockId);
        public Task<bool> CheckIfArticleExists(string article_url);
        public Task<bool> CheckIfStockArticleExists(string article_url, int stockId);
    }
}