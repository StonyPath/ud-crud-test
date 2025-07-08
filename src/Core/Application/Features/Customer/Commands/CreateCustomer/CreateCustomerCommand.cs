using Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace Application.Features.Customer.Commands.CreateCustomer;

public record CreateCustomerCommand(string FirstName,
                                    string LastName,
                                    DateTime DateOfBirth,
                                    string CountryCode,
                                    string PhoneNumber,
                                    string Email,
                                    string BankAccountNumber) 
                                   : IRequest<CustomerId>;
