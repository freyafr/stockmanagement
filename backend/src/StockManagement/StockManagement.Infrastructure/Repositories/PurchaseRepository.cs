using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Models;

namespace StockManagement.Infrastructure.Repositories
{
    public class PurchaseRepository(StockManagementDbContext context) : IPurchaseRepository
    {
        public async Task<IEnumerable<Purchase>> GetClientPurchases(Guid clientId)
        {
            return await context.Purchases.Include(x => x.Stock).Where(x => x.ClientId == clientId).OrderBy(x => x.PurchaseDate).ToListAsync();
        }

        public async Task AddPurchase(Purchase purchase)
        {
            await context.Purchases.AddAsync(purchase);
        }
    }
}
