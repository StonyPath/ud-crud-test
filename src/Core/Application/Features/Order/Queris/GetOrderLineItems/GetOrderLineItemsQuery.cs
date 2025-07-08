using Application.Models;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.Order.Queris.GetOrderLineItems;

public record GetOrderLineItemsQuery(OrderId OrderId) : IRequest<OrderDto> { }
