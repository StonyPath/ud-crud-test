using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.ValueObjects;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Services;

public class CustomerUniquenessCheckerService : ICustomerUniquenessCheckerService
{
    private readonly AppDbContext _context;
    public CustomerUniquenessCheckerService(AppDbContext context)
    {
        _context = context;
    }

    public bool IsEmailUnique(Email email)
    {
        return !_context.Customers.Any(c => c.Email.Value.ToLower() == email.Value.ToLower());
    }

    public bool IsPersonalInfoUnique(FirstName firstName, LastName lastName, DateOfBirth dateOfBirth)
    {
        return !_context.Customers.Any(c => c.FirstName.Value.Equals(firstName.Value)
                                        && c.LastName.Value.Equals(lastName.Value)
                                        && c.DateOfBirth.Value.Date == dateOfBirth.Value.Date);
    }
}
