using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;

namespace Server.Services
{
    public interface IDbWatchlistService
    {
        public Task<List<StockDetailsDTO>> GetWatchlistStocksUser();
        public Task AddStockToWatchlist(StockDetailsDTO stock);
        public Task RemoveStockFromWatchlist(string ticker);
    }
}