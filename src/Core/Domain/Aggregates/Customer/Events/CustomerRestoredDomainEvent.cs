using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Customer.Events;

internal class CustomerRestoredDomainEvent : DomainEventBase
{
    public CustomerId CustomerId { get; }
    public CustomerRestoredDomainEvent(CustomerId customerId) => CustomerId = customerId;
}
