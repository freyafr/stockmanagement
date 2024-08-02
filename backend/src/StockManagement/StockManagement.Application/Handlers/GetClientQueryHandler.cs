using AutoMapper;
using FluentValidation;
using MediatR;
using StockManagement.Application.Queries;
using StockManagement.Core.Dtos;
using StockManagement.Core.Interfaces;

namespace StockManagement.Application.Handlers;
public class GetClientQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<GetClientQuery> validator) : IRequestHandler<GetClientQuery, ClientDto>
{
    public async Task<ClientDto> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            throw new ValidationException("Validation failed when trying to get client information", validation.Errors);
        }
        var client = await unitOfWork.ClientRepository.GetClient(request.UserId);
        var result = mapper.Map<ClientDto>(client);
        return result;
    }
}
