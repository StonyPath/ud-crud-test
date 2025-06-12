using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Aggregates.Customer.ValueObjects;

public class BankAccountNumber : ValueObject
{
    public string Value { get; }
    public BankAccountNumber(string accountNumber)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Bank account number is required.", nameof(accountNumber));
        if (!IsValidFormat(accountNumber))
            throw new ArgumentException("Invalid bank account number format.", nameof(accountNumber));
        Value = accountNumber;
    }

    private bool IsValidFormat(string accountNumber)
    {
        // Example validation: only digits and length between 8 and 20
        return Regex.IsMatch(accountNumber, @"^\d{8,20}$");
    }
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
