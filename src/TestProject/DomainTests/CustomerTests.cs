using Domain.Aggregates.Customer.Entities;
using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.ValueObjects;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DomainTests;

public class CustomerTests
{
    [Fact]
    public void CreateCustomer_Should_CreateCustomerSuccessfully()
    {
        // Arrange
        var firstNameVal = "John";
        var lastNameVal = "Doe";
        var dateOfBirthVal = DateTime.UtcNow.AddYears(-25);
        var countryCode = "+1";
        var phoneNumberVal = "1234567890";
        var emailVal = "john.doe@example.com";
        var bankAccountVal = "1234567890";

        var firstName = new FirstName(firstNameVal);
        var lastName = new LastName(lastNameVal);
        var dateOfBirth = new DateOfBirth(dateOfBirthVal);
        var phoneNumber = new PhoneNumber(countryCode, phoneNumberVal);
        var email = new Email(emailVal);
        var bankAccountNumber = new BankAccountNumber(bankAccountVal);

        var uniquenessCheckerMock = new Mock<ICustomerUniquenessCheckerService>();
        uniquenessCheckerMock.Setup(x => x.IsEmailUnique(It.IsAny<Email>())).Returns(true);
        uniquenessCheckerMock.Setup(x => x.IsPersonalInfoUnique(It.IsAny<FirstName>(), It.IsAny<LastName>(), It.IsAny<DateOfBirth>())).Returns(true);

        // Act
        var customer = Customer.Create(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber, uniquenessCheckerMock.Object);

        // Assert
        customer.Should().NotBeNull();
        customer.FirstName.Value.Should().Be(firstNameVal);
    }
}
