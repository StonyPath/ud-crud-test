using Application.Features.Customer.Commands.CreateCustomer;
using Application.Features.Customer.Commands.DeleteCustomer;
using Application.Features.Customer.Commands.RestoreCustomer;
using Application.Features.Customer.Commands.UpdateCustomer;
using Application.Features.Customer.Queries.GetCustomerByEmail;
using Application.Features.Customer.Queries.GetCustomerById;
using Application.Features.Customer.Queries.GetCustomersList;
using Application.Models;
using Domain.Aggregates.Customer.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        CustomerId cId = new(customerId);

        if (cId != command.CustomerId)  return BadRequest();

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{customerId:guid}")]
    public async Task<IActionResult> DeleteCustomer(CustomerId customerId)
    {
        var command = new DeleteCustomerCommand(customerId );
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("restore")]
    public async Task<ActionResult<CustomerDto>> RestoreCustomer(CustomerId customerId)
    {
        var command = new RestoreCustomerCommand { CustomerId = customerId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetCustomerById{customerId:guid}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(Guid customerId)
    {
        CustomerId cId = new(customerId);
        var query = new GetCustomerByIdQuery(cId);
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
