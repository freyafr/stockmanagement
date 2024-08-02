using AutoMapper;
using MediatR;
using StockManagement.Application.Queries;
using StockManagement.Core.Dtos;
using StockManagement.Core.Interfaces;

namespace StockManagement.Application.Handlers;
public class GetStocksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetStocksQuery, IEnumerable<StockDto>>
{
    public async Task<IEnumerable<StockDto>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
    {
        var stocks = await unitOfWork.StocksRepository.GetStocks();
        var result = mapper.Map<IEnumerable<StockDto>>(stocks);
        return result;
    }
}
