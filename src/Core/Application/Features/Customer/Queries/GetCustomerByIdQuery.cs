﻿using Application.Features.Customer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Queries;

public record GetCustomerByIdQuery(Guid CustomerId) : IRequest<CustomerDto>;
