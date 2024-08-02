using MediatR;
using StockManagement.Core.Dtos;

namespace StockManagement.Application.Queries
{
    public class GetStocksQuery : IRequest<IEnumerable<StockDto>>
    {

    }
}
