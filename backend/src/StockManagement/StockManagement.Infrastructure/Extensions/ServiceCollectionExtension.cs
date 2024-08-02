using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Models;
using StockManagement.Infrastructure.Repositories;

namespace StockManagement.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IStocksRepository, StocksRepository>()
                .AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<IPurchaseRepository, PurchaseRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection InitDatabase(this IServiceCollection services)
        {
            services = services.AddEntityFrameworkInMemoryDatabase().AddDbContext<StockManagementDbContext>(
                options => options.UseInMemoryDatabase("StockManagement"));

            var provider = services.BuildServiceProvider();
            using (var serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<StockManagementDbContext>()!)
                {
                    AddTestData(context);
                }
            }

            return services;
        }

        private static void AddTestData(StockManagementDbContext context)
        {
            // adding init data
            var stocks = new List<Stock>{ new()
            {
                Name = "Apple",
                Price = 100
            },
                new()
                {
                    Name = "Google",
                    Price = 200
                }
            };

            var client = new Client
            {
                Id = Guid.Parse("89e8d857-3e13-4d47-b53b-04b0eda248ea"),
                Name = "John",
                LastName = "Snow",
                Balance = 3000,
                Email = "test@gmail.com"
            };
            context.AddRange(stocks);
            context.Add(client);
            context.SaveChanges();
        }
    }
}
