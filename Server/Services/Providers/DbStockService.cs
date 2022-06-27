using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjektAbpd.Server.Data;
using ProjektAbpd.Server.Models;
using ProjektAbpd.Server.Services;
using ProjektAbpd.Shared.Models;
using ProjektAbpd.Shared.Models.DTOs;
using Server.Models;

namespace Server.Services.Providers
{
    public class DbStockService : IDbStockService
    {
        private readonly ApplicationDbContext _context;
        public DbStockService(ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
        }

        public async Task AddStock(StockDetailsDTO stock)
        {
            await _context.Stocks.AddAsync(new DbStock
            {
                Name = stock.name,
                Ticker = stock.ticker,
                HomepageUrl = stock.homepage_url,
                PhoneNumber = stock.phone_number,
                Category = stock.sic_description,
                PrimaryExchange = stock.primary_exchange
            });
        }

        public async Task<int> GetStockIdByTicker(string ticker)
        {
            return await _context.Stocks.Where(e => e.Ticker == ticker)
                        .Select(e => e.StockId).SingleAsync();
        }

        public async Task<StockDetailsDTO> GetStockByTicker(string ticker)
        {
            return await _context.Stocks.Where(e => e.Ticker == ticker)
                    .Select(e => new StockDetailsDTO
                    {
                        name = e.Name,
                        sic_description = e.Category,
                        phone_number = e.PhoneNumber,
                        homepage_url = e.HomepageUrl,
                        ticker = ticker
                    }).SingleOrDefaultAsync();
        }

        public async Task<List<StockDetailsDTO>> GetAllStocksLikeTicker(string ticker)
        {
            return await _context.Stocks.Where(e => EF.Functions.Like(e.Ticker, $"{ticker}%"))
                .Select(e => new StockDetailsDTO
                {
                    name = e.Name,
                    sic_description = e.Category,
                    phone_number = e.PhoneNumber,
                    homepage_url = e.HomepageUrl,
                    primary_exchange = e.PrimaryExchange,
                    ticker = e.Ticker
                }).ToListAsync();
        }
    }
}