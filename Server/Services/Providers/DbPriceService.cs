using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektAbpd.Server.Data;
using ProjektAbpd.Shared.Models.DTOs;

namespace Server.Services.Providers
{
    public class DbPriceService : IDbPriceService
    {
        private readonly ApplicationDbContext _context;

        public DbPriceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfPriceExists(int stockId, DateTime date)
        {
            return await _context.StockPrices.AnyAsync(e => e.StockId == stockId && e.Date == date);
        }

        public async Task<List<PriceDTO>> GetStockPricesById(int stockId)
        {
            return await _context.StockPrices.Where(e => e.StockId == stockId)
                        .OrderBy(e => e.Date)
                        .Take(90)
                        .Select(p => new PriceDTO
                        {
                            Date = p.Date,
                            o = p.Open,
                            c = p.Close,
                            h = p.High,
                            l = p.Low,
                            v = p.Volume
                        }).ToListAsync();
        }
    }
}