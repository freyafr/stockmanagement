namespace StockManagement.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IStocksRepository StocksRepository { get; }
        IClientRepository ClientRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
