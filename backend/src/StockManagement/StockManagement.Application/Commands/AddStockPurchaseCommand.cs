using FluentValidation;
using MediatR;
using StockManagement.Core.Dtos;

namespace StockManagement.Application.Commands;

public class AddStockPurchaseCommand : IRequest<PurchaseCreateResponseDto>
{
    public required PurchaseCreateDto Purchase { get; init; }
    public bool ValidateOnly { get; init; }
}

public class AddStockPurchaseCommandValidator : AbstractValidator<AddStockPurchaseCommand>
{
    public AddStockPurchaseCommandValidator()
    {
        RuleFor(x => x.Purchase.Amount).GreaterThan(0);
        RuleFor(x => x.Purchase.ClientId).NotEqual(Guid.Empty);
        RuleFor(x => x.Purchase.StockId).NotEqual(Guid.Empty);
    }
}
