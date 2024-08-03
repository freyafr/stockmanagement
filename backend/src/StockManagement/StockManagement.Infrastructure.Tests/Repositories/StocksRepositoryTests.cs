using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using StockManagement.Core.Exceptions;
using StockManagement.Core.Models;
using StockManagement.Infrastructure.Repositories;

namespace StockManagement.Infrastructure.Tests.Repositories
{
    public class StocksRepositoryTests
    {
        private readonly StocksRepository _sub;
        private readonly Stock _existingStock = new()
        {
            Name = "Test 1",
            Price = 5
        };

        public StocksRepositoryTests()
        {
            var stocks = new List<Stock> {
                _existingStock,
                new ()
                {
                    Name = "Test 2",
                    Price = 7
                }
            };

            Mock<StockManagementDbContext> dbContextMock = new(new DbContextOptions<StockManagementDbContext>());
            var stocksDbSetMock = stocks.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Stocks).Returns(stocksDbSetMock.Object);

            _sub = new StocksRepository(dbContextMock.Object);
        }

        [Fact]
        public async Task GetStocks()
        {
            var result = await _sub.GetStocks();
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetStockExisting()
        {
            var result = await _sub.GetStock(_existingStock.Id);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetStockNotFound()
        {
            var action = () => _sub.GetStock(Guid.NewGuid());
            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}