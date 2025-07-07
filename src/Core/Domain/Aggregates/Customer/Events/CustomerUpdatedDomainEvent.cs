using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Customer.Events;

internal class CustomerUpdatedDomainEvent : DomainEventBase
{
    public CustomerId CustomerId { get; }
    public CustomerUpdatedDomainEvent(CustomerId customerId) => CustomerId = customerId;
}
