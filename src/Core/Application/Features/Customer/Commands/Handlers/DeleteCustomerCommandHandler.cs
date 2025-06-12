using Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.Entities;

namespace Application.Features.Customer.Commands.Handlers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer == null) throw new Exception("Customer not found");

        customer.Delete();
        await _customerRepository.SaveChangesAsync();
    }
}
