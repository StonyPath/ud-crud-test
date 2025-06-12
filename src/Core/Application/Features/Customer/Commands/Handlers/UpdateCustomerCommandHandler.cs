using Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.Entities;
using Application.Features.Customer.Models;

namespace Application.Features.Customer.Commands.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerUniquenessCheckerService _uniquenessChecker;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerUniquenessCheckerService uniquenessChecker)
    {
        _customerRepository = customerRepository;
        _uniquenessChecker = uniquenessChecker;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null) throw new Exception("Customer not found");

        // Create value objects from the command payload
        var firstName = new FirstName(request.FirstName);
        var lastName = new LastName(request.LastName);
        var dateOfBirth = new DateOfBirth(request.DateOfBirth);
        var phoneNumber = new PhoneNumber(request.CountryCode, request.PhoneNumber);
        var email = new Email(request.Email);
        var bankAccountNumber = new BankAccountNumber(request.BankAccountNumber);

        customer.ChangeAttribute(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber, _uniquenessChecker);
        await _customerRepository.UpdateAsync(customer);
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName.Value,
            LastName = customer.LastName.Value,
            DateOfBirth = customer.DateOfBirth.Value,
            CountryCode = customer.PhoneNumber.CountryCode,
            PhoneNumber = customer.PhoneNumber.Number,
            Email = customer.Email.Value,
            BankAccountNumber = customer.BankAccountNumber.Value,
            IsDeleted = customer.IsDeleted
        };
    }
}
