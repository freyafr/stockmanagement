using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace StockManagement.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(ServiceCollectionExtension).Assembly)
                .AddMediatR(typeof(ServiceCollectionExtension).Assembly)
                .AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);
        }
    }
}
