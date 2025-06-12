using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string Number { get; }
    public PhoneNumber(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            throw new ArgumentException("Country code is required.", nameof(countryCode));
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number is required.", nameof(number));
        if (countryCode != "+98") // Assuming Iran country code
            throw new ArgumentException("Country code must be '+98' for Iran.");

        // Basic validation for phone number parts, can be enhanced
        if (!Regex.IsMatch(number, @"^\d+$"))
            throw new ArgumentException("Phone number must contain only digits.", nameof(number));

        CountryCode = countryCode;
        Number = number;
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return CountryCode;
        yield return Number;
    }
}
