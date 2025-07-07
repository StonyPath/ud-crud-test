using Domain.Aggregates.Customer.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Customer.Events;

public class CustomerCreatedDomainEvent : DomainEventBase
{
    public CustomerId CustomerId { get; }
    public CustomerCreatedDomainEvent(CustomerId customerId) => CustomerId = customerId;
}
