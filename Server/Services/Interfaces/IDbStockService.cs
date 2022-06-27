using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models;
using ProjektAbpd.Shared.Models.DTOs;

namespace ProjektAbpd.Server.Services
{
    public interface IDbStockService
    {
        public Task AddStock(StockDetailsDTO stock);
        public Task<int> GetStockIdByTicker(string ticker);
        public Task<StockDetailsDTO> GetStockByTicker(string ticker);
        public Task<List<StockDetailsDTO>> GetAllStocksLikeTicker(string ticker);
    }
}