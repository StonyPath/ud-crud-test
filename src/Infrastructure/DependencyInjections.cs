using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Products.Entities;
using Domain.SeedWork;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjections
{
    public static IServiceCollection InfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
