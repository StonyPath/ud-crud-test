using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.Events;

public class CustomerCreatedDomainEvent : DomainEventBase
{
    public Guid CustomerId { get; }
    public CustomerCreatedDomainEvent(Guid customerId) => CustomerId = customerId;
}
