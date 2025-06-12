using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Customer.Rules;

public class CustomerPersonalInfoMustBeUniqueRule : IBusinessRule
{
    private readonly FirstName _firstName;
    private readonly LastName _lastName;
    private readonly DateOfBirth _dateOfBirth;
    private readonly ICustomerUniquenessCheckerService _checker;

    public CustomerPersonalInfoMustBeUniqueRule(FirstName firstName, LastName lastName, DateOfBirth dateOfBirth, ICustomerUniquenessCheckerService checker)
    {
        _firstName = firstName;
        _lastName = lastName;
        _dateOfBirth = dateOfBirth;
        _checker = checker;
    }

    public bool IsBroken()
    {
        return !_checker.IsPersonalInfoUnique(_firstName, _lastName, _dateOfBirth);
    }

    public string Message => "A customer with the same personal information already exists.";
}
