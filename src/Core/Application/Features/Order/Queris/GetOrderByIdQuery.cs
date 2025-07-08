using Application.Models;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;

namespace Application.Features.Order.Queris;

public record GetOrderByIdQuery(OrderId OrderId) : IRequest<OrderDto?> { }
