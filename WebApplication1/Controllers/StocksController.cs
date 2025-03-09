using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StockTracker2.Interfaces;
using WebApplication1.Models;
using static StockTracker2.Services.StockService;

namespace WebApplication1.Controllers
{
    public class StocksController : Controller
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<ActionResult> Index()
        {
            var stockSymbols = new List<string> { "AAPL", "MSFT", "NVDA", "AMZN", "GOOG" };
            var stocks = await _stockService.GetStocksAsync(stockSymbols);
            return View("~/Views/Home/Index.cshtml", stocks); // Explicit path to the view
        }
    }
}
