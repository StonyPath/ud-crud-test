using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Customer.ValueObjects;

namespace Infrastructure.Persistence.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer> GetByIdAsync(Guid id);
    Task<Customer> GetByEmailAsync(string email);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="take">PageSize</param>
    /// <param name="skip">PageNumber </param>
    /// <returns></returns>
    Task<(List<Customer> customers, int totalCount)> GetAllAsync(int take, int skip);
    Task UpdateAsync(Customer customer);
    Task SaveChangesAsync();
}
