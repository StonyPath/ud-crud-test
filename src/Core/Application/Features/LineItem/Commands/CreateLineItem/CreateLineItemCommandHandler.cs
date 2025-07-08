using Domain.Aggregates.LineItem.ValueObjects;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using MediatR;

namespace Application.Features.LineItem.Commands.CreateLineItem;

public class CreateLineItemCommandHandler : IRequestHandler<CreateLineItemCommand, LineItemId>
{
    public Task<LineItemId> Handle(CreateLineItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
