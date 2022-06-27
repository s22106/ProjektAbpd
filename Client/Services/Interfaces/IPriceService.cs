using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models.DTOs;

namespace ProjektAbpd.Client.Services
{
    public interface IPriceService
    {
        public Task<List<PriceDTO>> GetPricesForStock(string ticker);
        public Task AddStockPricesToDatabase(List<PriceDTO> stockPrices, string ticker);
    }
}