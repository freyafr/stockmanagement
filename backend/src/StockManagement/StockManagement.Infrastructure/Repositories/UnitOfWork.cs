using StockManagement.Core.Interfaces;

namespace StockManagement.Infrastructure.Repositories
{
    public class UnitOfWork(IStocksRepository stocksRepository, IClientRepository clientRepository, IPurchaseRepository purchaseRepository, StockManagementDbContext dbContext) : IUnitOfWork
    {
        public IStocksRepository StocksRepository { get; } = stocksRepository;

        public IClientRepository ClientRepository { get; } = clientRepository;

        public IPurchaseRepository PurchaseRepository { get; } = purchaseRepository;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
