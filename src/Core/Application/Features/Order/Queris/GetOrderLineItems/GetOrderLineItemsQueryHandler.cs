using Application.Models;
using Domain.Aggregates.Orders.Entities;
using MediatR;

namespace Application.Features.Order.Queris.GetOrderLineItems;

public class GetOrderLineItemsQueryHandler : IRequestHandler<GetOrderLineItemsQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderLineItemsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> Handle(GetOrderLineItemsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderLineItems(request.OrderId);

        if (order == null) return null;

        List<LineItemDto> lineItems = [.. order.LineItems.Select(li => new LineItemDto()
        {
            Id = li.Id,
            OrderId = li.OrderId,
            Price = li.Price,
            ProductId = li.ProductId
        })];

        return new OrderDto()
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            lineItems = lineItems
        };
    }
}
