using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models;
using ProjektAbpd.Shared.Models.DTOs;

namespace ProjektAbpd.Client.Services
{
    public interface IStockService
    {
        //TODO add model to clien
        public Task<List<Stock>> GetStocksListByTicker(string ticker);
        public Task<StockDetailsDTO> GetStockDetailsByTicker(string ticker);
        public Task AddStockToDatabase(StockDetailsDTO stock);
    }
}