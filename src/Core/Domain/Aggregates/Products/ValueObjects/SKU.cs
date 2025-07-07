using Domain.SeedWork;

namespace Domain.Aggregates.Products.ValueObjects;

//Stock Keeping Unit
public class SKU : ValueObject
{
    private const int DefaultLenght = 15;
    public string Value { get; }

    public SKU(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("SKU is required.", nameof(value));

        if (value.Length < DefaultLenght) throw new ArgumentException($"SKU min lenght should be over {DefaultLenght}");

        Value = value;
    }

    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
