using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.Order.Commands.RemoveLineItem;

public record RemoveLineItemCommand(OrderId OrderId, LineItemId LineItemId) : IRequest { }
