using StockManagement.Core.Models;

namespace StockManagement.Core.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetClientPurchases(Guid clientId);
        Task AddPurchase(Purchase purchase);
    }
}
