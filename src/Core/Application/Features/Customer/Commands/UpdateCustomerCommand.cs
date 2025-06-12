using Application.Features.Customer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Commands;

public record UpdateCustomerCommand(
    Guid CustomerId,
    string FirstName,
     string LastName,
     DateTime DateOfBirth,
     string CountryCode,
     string PhoneNumber,
     string Email,
     string BankAccountNumber
) : IRequest<CustomerDto>;
