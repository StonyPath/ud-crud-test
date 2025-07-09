using Domain.Aggregates.Orders.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Orders.Events;

internal class OrderCreatedDomainEvent : DomainEventBase
{
    public OrderId OrderId { get; }

    public OrderCreatedDomainEvent(OrderId orderId)=> OrderId = orderId;
}
