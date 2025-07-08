using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Customer.Entities;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    //Task AddAsync(Customer customer);
    //Task<Customer?> GetByIdAsync(CustomerId id);
    Task<Customer?> GetByEmailAsync(string email);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="take">PageSize</param>
    /// <param name="skip">PageNumber </param>
    /// <returns></returns>
    //Task<(List<Customer> customers, int totalCount)> GetAllAsync(int take, int skip);
    //Task UpdateAsync(Customer customer);
}
