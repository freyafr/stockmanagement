using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using StockManagement.Application.Commands;
using StockManagement.Application.Handlers;
using StockManagement.Core.Dtos;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Models;

namespace StockManagement.Application.Tests
{
    public class AddStockPurchaseCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<AddStockPurchaseCommand>> _validatorMock = new();

        private readonly PurchaseCreateDto _testPurchase = new PurchaseCreateDto
        {
            Amount = 2,
            ClientId = Guid.NewGuid(),
            StockId = Guid.NewGuid()
        };

        public AddStockPurchaseCommandHandlerTests()
        {
            var clientRepositoryMock = new Mock<IClientRepository>();
            _unitOfWorkMock.Setup(x => x.ClientRepository).Returns(clientRepositoryMock.Object);
            clientRepositoryMock.Setup(x => x.GetClient(_testPurchase.ClientId))
                .ReturnsAsync(new Client
                {
                    Id = _testPurchase.ClientId,
                    Email = "Test",
                    LastName = "Test",
                    Name = "",
                    Balance = 100
                });

            var stockRepositoryMock = new Mock<IStocksRepository>();
            _unitOfWorkMock.Setup(x => x.StocksRepository).Returns(stockRepositoryMock.Object);
            stockRepositoryMock.Setup(x => x.GetStock(_testPurchase.StockId))
                .ReturnsAsync(new Stock
                {
                    Name = "test",
                    Price = 12
                });
        }

        [Fact]
        public async Task HandleWithOnlyValidate()
        {
            _validatorMock.Setup(x => x.ValidateAsync(It.IsAny<AddStockPurchaseCommand>(), CancellationToken.None))
                .ReturnsAsync(new ValidationResult
                {
                    Errors = new()
                });

            var handler =
                new AddStockPurchaseCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object, _validatorMock.Object);
            var result = await handler.Handle(new AddStockPurchaseCommand
            {
                ValidateOnly = true,
                Purchase = _testPurchase
            }, CancellationToken.None);

            result.Should().NotBeNull();
            result.TotalPrice.Should().Be(24);
            result.ClientId.Should().Be(_testPurchase.ClientId);
            _unitOfWorkMock.Verify(x => x.PurchaseRepository, Times.Never);
        }
    }
}