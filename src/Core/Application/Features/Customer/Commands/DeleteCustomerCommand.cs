using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Commands;

public class DeleteCustomerCommand : IRequest
{
    public Guid CustomerId { get; set; }
}
