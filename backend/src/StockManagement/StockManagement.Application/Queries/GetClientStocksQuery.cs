using FluentValidation;
using MediatR;
using StockManagement.Core.Dtos;

namespace StockManagement.Application.Queries
{
    public class GetClientStocksQuery : IRequest<IEnumerable<PurchaseDto>>
    {
        public required Guid UserId { get; init; }
    }

    public class GetClientStocksQueryValidator : AbstractValidator<GetClientStocksQuery>
    {
        public GetClientStocksQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
