using Application.Features.Customer.Commands;
using Application.Features.Customer.Models;
using Application.Features.Customer.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCustomerById), new { customerId = customerId }, customerId);
    }

    [HttpPut("{customerId:guid}")]
    public async Task<ActionResult<CustomerDto>> UpdateCustomer(Guid customerId, [FromBody] UpdateCustomerCommand command)
    {
        if (customerId != command.CustomerId)
        {
            return BadRequest();
        }
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{customerId:guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid customerId)
    {
        var command = new DeleteCustomerCommand { CustomerId = customerId };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("restore/{customerId:guid}")]
    public async Task<ActionResult<CustomerDto>> RestoreCustomer(Guid customerId)
    {
        var command = new RestoreCustomerCommand { CustomerId = customerId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{customerId:guid}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(Guid customerId)
    {
        var query = new GetCustomerByIdQuery(customerId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("by-email")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByEmail([FromQuery] string email)
    {
        var query = new GetCustomerByEmailQuery(email);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedCustomersDto>> GetCustomersList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetCustomersListQuery(pageNumber, pageSize);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
