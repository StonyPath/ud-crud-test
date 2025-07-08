using Application.Models;
using Domain.Aggregates.Customer.Entities;
using MediatR;

namespace Application.Features.Customer.Queries.GetCustomersList;

public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, PaginatedCustomersDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersListQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<PaginatedCustomersDto> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        var customersInfo = await _customerRepository.GetAllAsync(request.PageSize,request.PageNumber);

        var customerDtos = customersInfo.customers.Select(customer => new CustomerDto
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
        }).ToList();

        return new PaginatedCustomersDto
        {
            Customers = customerDtos,
            TotalCount = customersInfo.totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
