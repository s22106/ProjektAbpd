using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektAbpd.Server.Data;

namespace Server.Services.Providers
{

    public class DbSharedService : IDbSharedService
    {
        private readonly ApplicationDbContext _context;

        public DbSharedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfStockExists(string ticker)
        {
            return await _context.Stocks.AnyAsync(e => e.Ticker == ticker);
        }

        public async Task CreateAsync<T>(T entry) where T : class
        {
            await _context.Set<T>().AddAsync(entry);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}