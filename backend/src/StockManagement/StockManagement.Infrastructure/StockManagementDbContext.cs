using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Models;

namespace StockManagement.Infrastructure
{
    public class StockManagementDbContext(DbContextOptions<StockManagementDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Stock> Stocks { get; init; } = null!;
        public virtual DbSet<Client> Clients { get; init; } = null!;
        public virtual DbSet<Purchase> Purchases { get; init; } = null!;
    }
}
