using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.Events;

internal class CustomerUpdatedDomainEvent : DomainEventBase
{
    public Guid CustomerId { get; }
    public CustomerUpdatedDomainEvent(Guid customerId) => CustomerId = customerId;
}
