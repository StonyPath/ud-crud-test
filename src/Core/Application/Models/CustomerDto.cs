using Domain.Aggregates.Customer.ValueObjects;

namespace Application.Models;

public class CustomerDto
{
    public CustomerId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CountryCode { get; internal set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
    public bool IsDeleted { get; set; }
}
