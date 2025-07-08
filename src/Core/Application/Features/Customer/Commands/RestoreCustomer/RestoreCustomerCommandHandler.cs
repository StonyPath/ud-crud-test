using Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.Entities;
using Application.Models;

namespace Application.Features.Customer.Commands.RestoreCustomer;

public class RestoreCustomerCommandHandler : IRequestHandler<RestoreCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;

    public RestoreCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto> Handle(RestoreCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null) throw new Exception("Customer not found");

        customer.Restore();
        await _customerRepository.SaveChangesAsync();
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
