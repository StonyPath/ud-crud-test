using Domain.Aggregates.Orders.Entities;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderId>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderId> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderId orderId = await _orderRepository.CreateOrder(request.CustomerId);
        return orderId;
    }
}
