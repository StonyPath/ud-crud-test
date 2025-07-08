using Application.Models;
using Domain.Aggregates.Orders.Entities;
using MediatR;

namespace Application.Features.Order.Queris.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, PaginatedOrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PaginatedOrderDto> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(request.PageSize, request.PageNumber);

        var ordersDto = orders.entities.Select(p => new OrderDto
        {
            Id = p.Id,
            CustomerId = p.CustomerId,
        }).ToList();

        return new PaginatedOrderDto
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = orders.totalCount,
            Orders = ordersDto
        };
    }
}
