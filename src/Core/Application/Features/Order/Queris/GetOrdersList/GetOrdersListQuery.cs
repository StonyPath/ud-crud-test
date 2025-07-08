using Application.Models;
using MediatR;

namespace Application.Features.Order.Queris.GetOrdersList;

public record GetOrdersListQuery(int PageNumber, int PageSize) : IRequest<PaginatedOrderDto> {}
