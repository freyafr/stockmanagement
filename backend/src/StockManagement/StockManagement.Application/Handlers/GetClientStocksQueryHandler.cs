using AutoMapper;
using FluentValidation;
using MediatR;
using StockManagement.Application.Queries;
using StockManagement.Core.Dtos;
using StockManagement.Core.Interfaces;

namespace StockManagement.Application.Handlers;
public class GetClientStocksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<GetClientStocksQuery> validator) : IRequestHandler<GetClientStocksQuery, IEnumerable<PurchaseDto>>
{
    public async Task<IEnumerable<PurchaseDto>> Handle(GetClientStocksQuery request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            throw new ValidationException("Validation failed when trying to get client stocks", validation.Errors);
        }
        var stocks = await unitOfWork.PurchaseRepository.GetClientPurchases(request.UserId);
        var result = mapper.Map<IEnumerable<PurchaseDto>>(stocks);
        return result;
    }
}
