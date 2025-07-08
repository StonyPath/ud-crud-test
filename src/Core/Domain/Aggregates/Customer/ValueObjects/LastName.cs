using Domain.SeedWork;

namespace Domain.Aggregates.Customer.ValueObjects;

public class LastName : ValueObject
{
    private LastName() { }  

    public string Value { get; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Last name cannot be empty.");
        Value = value;
    }
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
