using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Orders.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjections
{
    public static IServiceCollection InfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
