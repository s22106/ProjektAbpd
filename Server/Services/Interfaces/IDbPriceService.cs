using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;

namespace Server.Services
{
    public interface IDbPriceService
    {
        public Task<bool> CheckIfPriceExists(int stockId, DateTime date);
        public Task<List<PriceDTO>> GetStockPricesById(int stockId);
    }
}