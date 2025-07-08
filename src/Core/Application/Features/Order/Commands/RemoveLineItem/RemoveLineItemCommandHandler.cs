using Domain.Aggregates.Orders.Entities;
using MediatR;

namespace Application.Features.Order.Commands.RemoveLineItem;

public class RemoveLineItemCommandHandler : IRequestHandler<RemoveLineItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveLineItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(RemoveLineItemCommand request, CancellationToken cancellationToken)
    {
        await _orderRepository.RemoveLineItemAsync(request.OrderId, request.LineItemId);
    }
}
