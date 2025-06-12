using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.ValueObjects;

public class FirstName : ValueObject
{
    public string Value { get; }
    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("First name cannot be empty.");
        Value = value;
    }
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
