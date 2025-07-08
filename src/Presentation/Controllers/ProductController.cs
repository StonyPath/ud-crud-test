using Application.Features.Customer.Queries.GetCustomersList;
using Application.Features.Order.Commands.CreateOrder;
using Application.Features.Order.Queris.GetOrderById;
using Application.Features.Product.Commands.CreateProduct;
using Application.Features.Product.Queries.GetProductById;
using Application.Features.Product.Queries.GetProductsList;
using Application.Models;
using Domain.Aggregates.Orders.ValueObjects;
using Domain.Aggregates.Products.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateProduct")]
    public async Task<ActionResult<OrderId>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { productId.Value }, productId.Value);
    }

    [HttpGet("GetProduct")]
    public async Task<ActionResult<OrderDto>> GetProduct(Guid productId)
    {
        ProductId pId = new(productId);
        GetProductByIdQuery productByIdQuery = new (pId);
        ProductDto? producct = await _mediator.Send(productByIdQuery);
        return Ok(producct);
    }

    [HttpGet("GetProductsList")]
    public async Task<ActionResult<PaginatedCustomersDto>> GetProductsList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetProductsLitsQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
