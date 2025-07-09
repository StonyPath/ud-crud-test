using Domain.Aggregates.Orders.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Events;

internal class AddLineItemEvent : DomainEventBase
{
    public LineItemId LineItemId { get; }

    public AddLineItemEvent(LineItemId lineItemId) => LineItemId = lineItemId;
}
