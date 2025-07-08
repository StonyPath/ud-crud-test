using Application.Models;
using Domain.Aggregates.Customer.ValueObjects;
using MediatR;

namespace Application.Features.Customer.Commands.UpdateCustomer;

public record UpdateCustomerCommand(CustomerId CustomerId,
                                    string FirstName,
                                    string LastName,
                                    DateTime DateOfBirth,
                                    string CountryCode,
                                    string PhoneNumber,
                                    string Email,
                                    string BankAccountNumber)
                                   : IRequest<CustomerDto>;
