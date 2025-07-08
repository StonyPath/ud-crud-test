using Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace Application.Features.Customer.Commands.DeleteCustomer;

public record DeleteCustomerCommand(CustomerId CustomerId) : IRequest {}
