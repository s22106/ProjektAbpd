using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;

namespace ProjektAbpd.Client.Services
{
    public interface IWatchlistService
    {
        public Task RemoveFromWatchlist(StockDetailsDTO stock);
        public Task AddToUserWatchlist(StockDetailsDTO stock);
        public Task<List<StockDetailsDTO>> RequestUserWatchlist();
    }
}