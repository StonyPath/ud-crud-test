using Domain.SeedWork;

namespace Domain.Aggregates.Products.ValueObjects;

public class Money : ValueObject
{
    public string Currency { get; }
    public decimal Amount { get; }
    public Money(string currency, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.", nameof(currency));

        Currency = currency;
        Amount = amount;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Currency;
        yield return Amount;
    }
}
