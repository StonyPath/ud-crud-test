using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using MediatR;

namespace Application.Features.Order.Commands.CreateLineItem;

public record CreateLineItemCommand(OrderId orderId, ProductId productId, Money price) : IRequest<LineItemId> { }
