using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjektAbpd.Server.Data;
using ProjektAbpd.Shared.Models.DTOs;
using Server.Models;

namespace Server.Services.Providers
{
    public class DbWatchlistService : IDbWatchlistService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ApplicationDbContext _context;

        public DbWatchlistService(IHttpContextAccessor accessor, ApplicationDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        public async Task AddStockToWatchlist(StockDetailsDTO stock)
        {
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var stockId = await _context.Stocks.Where(e => e.Ticker == stock.ticker).Select(e => e.StockId).FirstOrDefaultAsync();

            await _context.UserStocks.AddAsync(new DbUserStocks
            {
                UserId = userId,
                StockId = stockId
            });
        }

        public async Task<List<StockDetailsDTO>> GetWatchlistStocksUser()
        {
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Users
                .Where(e => e.Id == userId)
                .Include(e => e.Wishlisted)
                .ThenInclude(Wishlisted => Wishlisted.Stock)
                .Select(e => e.Wishlisted.Select(w => new StockDetailsDTO
                {
                    name = w.Stock.Name,
                    ticker = w.Stock.Ticker,
                    homepage_url = w.Stock.HomepageUrl,
                    phone_number = w.Stock.PhoneNumber,
                    sic_description = w.Stock.Category,
                    primary_exchange = w.Stock.PrimaryExchange
                }).ToList()).SingleAsync();
        }

        public async Task RemoveStockFromWatchlist(string ticker)
        {
            var userId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var watchlistEntry = await _context.UserStocks
                .Include(e => e.Stock)
                .Where(e => e.Stock.Ticker == ticker && e.UserId == userId)
                .Select(e => new DbStock { StockId = e.Stock.StockId }).FirstAsync();
            _context.Entry(watchlistEntry).State = EntityState.Deleted;
        }
    }
}