using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.LineItem.Commands.RemoveLineItem;

public record RemoveLineItemCommand(OrderId OrderId, LineItemId LineItemId) : IRequest { }
