using Application.Features.Order.Commands.CreateLineItem;
using Application.Features.Order.Commands.CreateOrder;
using Application.Features.Order.Queris.GetOrderById;
using Application.Features.Order.Queris.GetOrderLineItems;
using Application.Features.Order.Queris.GetOrdersList;
using Application.Features.Product.Queries.GetProductsList;
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

    [HttpPost("CreateOrder")]
    public async Task<ActionResult<OrderId>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { orderId.Value }, orderId.Value);
    }

    [HttpGet("GetOrder")]
    public async Task<ActionResult<OrderDto>> GetOrder(Guid orderId)
    {
        OrderId oId = new(orderId);
        GetOrderByIdQuery orderByIdQuery = new GetOrderByIdQuery(oId);
        OrderDto? order = await _mediator.Send(orderByIdQuery);
        return Ok(order);
    }

    [HttpPost("CreateLineItem")]
    public async Task<ActionResult<LineItemId>> CreateLineItem([FromBody] CreateLineItemCommand command)
    {
        var lineItemId = await _mediator.Send(command);
        return Ok(lineItemId);
    }

    [HttpGet("GetOrderLineItems")]
    public async Task<ActionResult<OrderDto>> GetOrderLineItems(Guid orderId)
    {
        OrderId oId = new(orderId);
        GetOrderLineItemsQuery orderLineItemsQuery = new(oId);
        OrderDto orderDto = await _mediator.Send(orderLineItemsQuery);
        return Ok(orderDto);
    }

    [HttpGet("GetOrdersList")]
    public async Task<ActionResult<PaginatedCustomersDto>> GetOrdersList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetOrdersListQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
