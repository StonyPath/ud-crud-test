using Application.Features.Customer.Models;
using Domain.Aggregates.Customer.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Queries.Handlers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new Exception($"Customer with id {request.CustomerId} not found.");

        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName.Value,
            LastName = customer.LastName.Value,
            DateOfBirth = customer.DateOfBirth.Value,
            CountryCode = customer.PhoneNumber.CountryCode,
            PhoneNumber = $"{customer.PhoneNumber.CountryCode} {customer.PhoneNumber.Number}",
            Email = customer.Email.Value,
            BankAccountNumber = customer.BankAccountNumber.Value,
            IsDeleted = customer.IsDeleted
        };
    }
}
