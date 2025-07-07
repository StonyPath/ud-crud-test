using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Customer.Events;

internal class CustomerDeletedDomainEvent : DomainEventBase
{
    public CustomerId CustomerId { get; }
    public CustomerDeletedDomainEvent(CustomerId customerId) => CustomerId = customerId;
}
