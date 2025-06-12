using Domain.Aggregates.Customer.ValueObjects;

namespace Domain.Aggregates.Customer.Services;

public interface ICustomerUniquenessCheckerService
{
    bool IsEmailUnique(Email email);
    bool IsPersonalInfoUnique(FirstName firstName, LastName lastName, DateOfBirth dateOfBirth);
}

