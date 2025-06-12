using Domain.Aggregates.Customer.Events;
using Domain.Aggregates.Customer.Rules;
using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.Entities;

public class Customer : AggregateRoot<Guid>, ISoftDelete
{
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public BankAccountNumber BankAccountNumber { get; private set; }
    public bool IsDeleted { get; private set; }

    private Customer(
        FirstName firstName,
        LastName lastName,
        DateOfBirth dateOfBirth,
        PhoneNumber phoneNumber,
        Email email,
        BankAccountNumber bankAccountNumber)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
        IsDeleted = false;
    }

    public static Customer Create(
        FirstName firstName,
        LastName lastName,
        DateOfBirth dateOfBirth,
        PhoneNumber phoneNumber,
        Email email,
        BankAccountNumber bankAccountNumber,
        ICustomerUniquenessCheckerService uniquenessChecker)
    {
        // Enforce business rules
        var emailRule = new CustomerEmailMustBeUniqueRule(email, uniquenessChecker);

        if (emailRule.IsBroken()) throw new Exception(emailRule.Message);

        var personalInfoRule = new CustomerPersonalInfoMustBeUniqueRule(firstName, lastName, dateOfBirth, uniquenessChecker);

        if (personalInfoRule.IsBroken()) throw new Exception(personalInfoRule.Message);

        var customer = new Customer(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);
        customer.AddDomainEvent(new CustomerCreatedDomainEvent(customer.Id));
        return customer;
    }

    public void ChangeAttribute(
        FirstName firstName,
        LastName lastName,
        DateOfBirth dateOfBirth,
        PhoneNumber phoneNumber,
        Email email,
        BankAccountNumber bankAccountNumber,
        ICustomerUniquenessCheckerService uniquenessChecker)
    {
        var emailRule = new CustomerEmailMustBeUniqueRule(email, uniquenessChecker);

        if (emailRule.IsBroken()) throw new Exception(emailRule.Message);

        var personalInfoRule = new CustomerPersonalInfoMustBeUniqueRule(firstName, lastName, dateOfBirth, uniquenessChecker);

        if (personalInfoRule.IsBroken()) throw new Exception(personalInfoRule.Message);

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;

        AddDomainEvent(new CustomerUpdatedDomainEvent(this.Id));
    }

    public void Delete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            AddDomainEvent(new CustomerDeletedDomainEvent(this.Id));
        }
    }

    public void Restore()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            AddDomainEvent(new CustomerRestoredDomainEvent(this.Id));
        }
    }
}
