using StockTracker2.Models;
namespace StockTracker2.Interfaces
{
    public interface IStockService
    {
        Task<List<Stocks>> GetStocksAsync(List<string> symbols);
    }
}
