using Application.Models;
using Domain.Aggregates.Customer.Entities;
using MediatR;

namespace Application.Features.Customer.Queries.GetCustomerByEmail;

public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByEmailQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByEmailAsync(request.Email);

        if (customer == null) throw new Exception("Customer not found");

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
