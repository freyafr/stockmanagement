using FluentValidation;
using MediatR;
using StockManagement.Core.Dtos;

namespace StockManagement.Application.Queries
{
    public class GetClientQuery : IRequest<ClientDto>
    {
        public required Guid UserId { get; init; }
    }

    public class GetClientQueryValidator : AbstractValidator<GetClientQuery>
    {
        public GetClientQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        }
    }
}
