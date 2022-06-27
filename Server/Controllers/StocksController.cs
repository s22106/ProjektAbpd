using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjektAbpd.Server.Services;
using ProjektAbpd.Shared.Models.DTOs;
using Server.Models;
using Server.Services;
//using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IDbStockService _stockService;
        private readonly IDbSharedService _sharedService;
        private readonly IDbPriceService _priceService;
        public StocksController(IDbStockService stockService, IDbSharedService sharedService, IDbPriceService priceService)
        {
            _stockService = stockService;
            _sharedService = sharedService;
            _priceService = priceService;
        }

        [HttpGet("{ticker}/all")]
        public async Task<ActionResult<StockDetailsDTO>> GetAllStocksLikeTicker(string ticker)
        {
            var body = await _stockService.GetAllStocksLikeTicker(ticker);

            return new JsonResult(body, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }

        [HttpGet("{ticker}")]
        public async Task<ActionResult<StockDetailsDTO>> GetStockByTicker(string ticker)
        {
            if (!await _sharedService.CheckIfStockExists(ticker))
            {
                return NotFound("Stock does not exist in database");
            }

            return await _stockService.GetStockByTicker(ticker);
        }
        [HttpPost("")]
        public async Task<ActionResult> AddStock(StockDetailsDTO stock)
        {
            if (!ModelState.IsValid)
            {
                return Conflict("Wrong model");
            }
            if (await _sharedService.CheckIfStockExists(stock.ticker))
            {
                return NotFound("Stock already exists in database");
            }

            await _sharedService.CreateAsync(new DbStock
            {
                Name = stock.name,
                HomepageUrl = stock.homepage_url,
                PhoneNumber = stock.phone_number,
                Category = stock.sic_description,
                Ticker = stock.ticker,
                PrimaryExchange = stock.primary_exchange
            });

            await _sharedService.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{ticker}/Prices")]
        public async Task<ActionResult<List<PriceDTO>>> GetPricesByTicker(string ticker)
        {
            if (!await _sharedService.CheckIfStockExists(ticker))
            {
                return NotFound("Stock does not exist in database");
            }
            var stockId = await _stockService.GetStockIdByTicker(ticker);

            var body = _priceService.GetStockPricesById(stockId);

            return Ok(body);
        }

        [HttpPost("{ticker}/Prices")]
        public async Task<IActionResult> AddStockPrices(string ticker, List<PriceDTO> prices)
        {

            var stockId = await _stockService.GetStockIdByTicker(ticker);
            var sorted = prices.OrderBy(e => e.Date);
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (var price in prices)
                    {
                        if (await _priceService.CheckIfPriceExists(stockId, price.Date.Date)) { break; }
                        await _sharedService.CreateAsync(new DbStockPrice
                        {
                            StockId = stockId,
                            Date = price.Date.Date,
                            Close = price.c,
                            Open = price.o,
                            Low = price.l,
                            High = price.h,
                            Volume = price.v
                        });
                    }
                    transaction.Complete();
                }
                catch (Exception)
                {
                    return Problem("Server error");
                }
            }
            await _sharedService.SaveChangesAsync();

            return NoContent();
        }


    }
}