using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAbpd.Server.Services;
using ProjektAbpd.Shared.Models;
using ProjektAbpd.Shared.Models.DTOs;
using Server.Services;
//using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IDbStockService _stockService;
        private readonly IDbSharedService _sharedService;
        private readonly IDbWatchlistService _watchlistService;
        public WatchlistController(IDbStockService stockService, IDbSharedService sharedService, IDbWatchlistService watchlistService)
        {
            _stockService = stockService;
            _sharedService = sharedService;
            _watchlistService = watchlistService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<StockDetailsDTO>>> GetUserWatchlist()
        {

            var body = await _watchlistService.GetWatchlistStocksUser();
            return Ok(body);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddStockToWatchlist(StockDetailsDTO stock)
        {


            await _watchlistService.AddStockToWatchlist(stock);

            await _sharedService.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{ticker}")]
        public async Task<IActionResult> RemoveFromWatchlist(string ticker)
        {
            await _watchlistService.RemoveStockFromWatchlist(ticker);

            await _sharedService.SaveChangesAsync();
            return NoContent();
        }

    }
}