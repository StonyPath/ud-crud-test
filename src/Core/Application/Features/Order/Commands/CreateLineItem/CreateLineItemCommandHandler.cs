using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.Order.Commands.CreateLineItem;

public class CreateLineItemCommandHandler : IRequestHandler<CreateLineItemCommand, LineItemId>
{
    private readonly IOrderRepository _orderRepository;

    public CreateLineItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<LineItemId> Handle(CreateLineItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.orderId);

        if (order == null) return null;

        var lineItemId = order.AddLineItem(request.productId, request.price);
        await _orderRepository.SaveChangesAsync();
        return lineItemId;
    }
}
