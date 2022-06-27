using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;

namespace ProjektAbpd.Client.Services
{
    public interface IArticleService
    {
        public Task<List<ArticleDTO>> GetArticlesByTicker(string ticker);
        public Task AddArticlesToDatabase(List<ArticleDTO> articles, string ticker);
    }
}