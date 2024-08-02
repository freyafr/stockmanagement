using AutoMapper;
using FluentValidation;
using MediatR;
using StockManagement.Application.Commands;
using StockManagement.Core.Dtos;
using StockManagement.Core.Exceptions;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Models;

namespace StockManagement.Application.Handlers
{
    public class AddStockPurchaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AddStockPurchaseCommand> validator) : IRequestHandler<AddStockPurchaseCommand, PurchaseCreateResponseDto>
    {
        public async Task<PurchaseCreateResponseDto> Handle(AddStockPurchaseCommand request, CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
            {
                throw new ValidationException("Validation failed when trying to add a purchase", validation.Errors);
            }

            var client = await unitOfWork.ClientRepository.GetClient(request.Purchase.ClientId);
            var stock = await unitOfWork.StocksRepository.GetStock(request.Purchase.StockId);

            if (stock.Price * request.Purchase.Amount > client.Balance)
            {
                throw new NotEnoughMoneyException("Not enough money to purchase the requested amount of stocks");
            }

            if (request.ValidateOnly)
            {
                return new PurchaseCreateResponseDto
                {
                    ClientId = client.Id,
                    PurchaseId = Guid.Empty,
                    TotalPrice = stock.Price * request.Purchase.Amount,
                    PurchaseDate = DateTime.UtcNow
                };
            }

            var purchase = mapper.Map<Purchase>(request.Purchase);
            client.Balance -= stock.Price * request.Purchase.Amount;

            await unitOfWork.ClientRepository.UpdateClient(client);
            await unitOfWork.PurchaseRepository.AddPurchase(purchase);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new PurchaseCreateResponseDto
            {
                ClientId = client.Id,
                PurchaseId = purchase.Id,
                TotalPrice = stock.Price * request.Purchase.Amount,
                PurchaseDate = purchase.PurchaseDate
            };
        }
    }
}
