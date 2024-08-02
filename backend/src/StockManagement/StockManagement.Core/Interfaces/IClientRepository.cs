using StockManagement.Core.Models;

namespace StockManagement.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClient(Guid id);
        Task UpdateClient(Client client);
    }
}
