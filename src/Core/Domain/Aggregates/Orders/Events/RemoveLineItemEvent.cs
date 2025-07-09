using Domain.Aggregates.Orders.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Events;

internal class RemoveLineItemEvent : DomainEventBase
{
    public LineItemId LineItemId { get; }

    public RemoveLineItemEvent(LineItemId lineItemId) => LineItemId = lineItemId;
}
