using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Exceptions;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Models;

namespace StockManagement.Infrastructure.Repositories
{
    public class ClientRepository(StockManagementDbContext context) : IClientRepository
    {
        public async Task<Client> GetClient(Guid id)
        {
            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client is null)
            {
                throw new NotFoundException($"Client with Id={id} is not found");
            }

            return client;
        }

        public Task UpdateClient(Client client)
        {
            context.Clients.Update(client);
            return Task.CompletedTask;
        }
    }
}
