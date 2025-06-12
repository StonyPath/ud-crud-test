using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }
    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Last name cannot be empty.");
        Value = value;
    }
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
