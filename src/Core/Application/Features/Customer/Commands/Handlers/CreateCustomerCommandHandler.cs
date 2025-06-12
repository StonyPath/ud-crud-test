using Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Domain.Aggregates.Customer.Services;
using Domain.Aggregates.Customer.Entities;

namespace Application.Features.Customer.Commands.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerUniquenessCheckerService _uniquenessChecker;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerUniquenessCheckerService uniquenessChecker)
    {
        _customerRepository = customerRepository;
        _uniquenessChecker = uniquenessChecker;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Create value objects from the command payload
        var firstName = new FirstName(request.FirstName);
        var lastName = new LastName(request.LastName);
        var dateOfBirth = new DateOfBirth(request.DateOfBirth);
        var phoneNumber = new PhoneNumber(request.CountryCode, request.PhoneNumber);
        var email = new Email(request.Email);
        var bankAccountNumber = new BankAccountNumber(request.BankAccountNumber);

        var customer = Domain.Aggregates.Customer.Entities.Customer.Create(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber, _uniquenessChecker);
        await _customerRepository.AddAsync(customer);
        return customer.Id;
    }
}
