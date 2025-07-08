using Domain.Aggregates.Customer.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;
public record CreateOrderCommand(CustomerId CustomerId) : IRequest<OrderId> { }
