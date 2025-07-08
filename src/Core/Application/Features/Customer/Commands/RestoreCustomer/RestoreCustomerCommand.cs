using Application.Models;
using Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace Application.Features.Customer.Commands.RestoreCustomer;

public class RestoreCustomerCommand : IRequest<CustomerDto>
{
    public CustomerId CustomerId { get; set; }
}
