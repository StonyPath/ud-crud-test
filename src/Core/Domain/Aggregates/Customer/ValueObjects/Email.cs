using Domain.SeedWork;
using System.Text.RegularExpressions;

namespace Domain.Aggregates.Customer.ValueObjects;

public class Email : ValueObject
{
    private Email() { }

    public string Value { get; }

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format.", nameof(email));
        Value = email;
    }

    private bool IsValidEmail(string email)
    {
        // Simple regex for email validation
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
