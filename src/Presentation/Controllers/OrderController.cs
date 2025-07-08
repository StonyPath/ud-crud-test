using Application.Features.Order.Commands.CreateOrder;
using Application.Features.Order.Queris;
using Application.Models;
using Domain.Aggregates.Orders.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { orderId.Value }, orderId.Value);
    }


    [HttpGet("GetOrder")]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid orderId)
    {
        OrderId oId = new (orderId);
        GetOrderByIdQuery orderByIdQuery = new GetOrderByIdQuery(oId);
        OrderDto? order = await _mediator.Send(orderByIdQuery);
        return Ok(order);
    }
}
