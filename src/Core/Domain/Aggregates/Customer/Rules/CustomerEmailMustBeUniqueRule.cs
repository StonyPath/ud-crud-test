using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.Rules;

public class CustomerEmailMustBeUniqueRule : IBusinessRule
{
    private readonly Email _email;
    private readonly ICustomerUniquenessCheckerService _checker;

    public CustomerEmailMustBeUniqueRule(Email email, ICustomerUniquenessCheckerService checker)
    {
        _email = email;
        _checker = checker;
    }

    public bool IsBroken()
    {
        return !_checker.IsEmailUnique(_email);
    }

    public string Message => "The customer email must be unique.";
}
