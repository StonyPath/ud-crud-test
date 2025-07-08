﻿using Application.Models;
using MediatR;

namespace Application.Features.Customer.Queries.GetCustomerByEmail;

public record GetCustomerByEmailQuery(string Email) : IRequest<CustomerDto> { }
