using Domain.SeedWork;

namespace Domain.Aggregates.Customer.ValueObjects;

public class FirstName : ValueObject
{
    private FirstName() { } 

    public string Value { get; }

    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("First name cannot be empty.");
        Value = value;
    }
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
