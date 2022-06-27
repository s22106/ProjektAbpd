using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IDbSharedService
    {
        public Task<bool> CheckIfStockExists(string ticker);
        public Task SaveChangesAsync();
        public Task CreateAsync<T>(T entry) where T : class;
    }
}