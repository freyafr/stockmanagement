using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Exceptions;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Models;

namespace StockManagement.Infrastructure.Repositories
{
    public class StocksRepository(StockManagementDbContext context) : IStocksRepository
    {
        public async Task<IEnumerable<Stock>> GetStocks()
        {
            return await context.Stocks.ToListAsync();
        }

        public async Task<Stock> GetStock(Guid id)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock is null)
            {
                throw new NotFoundException($"Stock with Id={id} is not found");
            }

            return stock;
        }
    }
}
