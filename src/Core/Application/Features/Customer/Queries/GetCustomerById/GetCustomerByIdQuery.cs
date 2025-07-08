using Application.Models;
using Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace Application.Features.Customer.Queries.GetCustomerById;

public record GetCustomerByIdQuery(CustomerId CustomerId) : IRequest<CustomerDto>;
