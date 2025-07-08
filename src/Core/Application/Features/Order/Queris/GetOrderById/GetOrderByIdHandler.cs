using Application.Models;
using Domain.Aggregates.Orders.Entities;
using MediatR;

namespace Application.Features.Order.Queris.GetOrderById;
public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

        if (order == null) return null;

        return new OrderDto() { Id = order.Id, CustomerId = order.CustomerId };
    }
}
