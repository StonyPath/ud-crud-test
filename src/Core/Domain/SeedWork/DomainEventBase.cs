using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SeedWork;

public abstract class DomainEventBase
{
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
}
