using Domain.SeedWork;

namespace Domain.Aggregates.Customer.ValueObjects;

public class DateOfBirth : ValueObject
{
    public DateTime Value { get; }
    public DateOfBirth(DateTime dateOfBirth)
    {
        var age = CalculateAge(dateOfBirth, DateTime.Today);

        if (age < 15 || age > 80) throw new ArgumentException("Age must be between 15 and 80 years.");

        Value = dateOfBirth.Date;
    }

    private int CalculateAge(DateTime birthDate, DateTime currentDate)
    {
        var age = currentDate.Year - birthDate.Year;

        if (birthDate > currentDate.AddYears(-age)) age--;

        return age;
    }
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
