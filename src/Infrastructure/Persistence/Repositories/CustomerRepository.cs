using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Customer.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    public CustomerRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<Customer?> GetByIdAsync(CustomerId id)
        => await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Customer?> GetByEmailAsync(string email)
    => await _context.Customers.FirstOrDefaultAsync(x => x.Email.Value.ToLower() == email.ToLower());

    public async Task<(List<Customer> customers, int totalCount)> GetAllAsync(int take, int skip)
    {
        var query = _context.Customers.AsQueryable();
        var totalCount = await query.CountAsync();
        var customers = await query.Skip((skip - 1) * take)
                                   .Take(take)
                                   .ToListAsync();

        return (customers, totalCount);
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
