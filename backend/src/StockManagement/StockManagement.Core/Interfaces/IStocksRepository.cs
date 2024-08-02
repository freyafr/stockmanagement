using StockManagement.Core.Models;

namespace StockManagement.Core.Interfaces
{
    public interface IStocksRepository
    {
        Task<IEnumerable<Stock>> GetStocks();
        Task<Stock> GetStock(Guid id);
    }
}
