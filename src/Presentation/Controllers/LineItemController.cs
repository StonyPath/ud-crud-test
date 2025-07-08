using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LineItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public LineItemController(IMediator mediator)
    {
        _mediator = mediator;
    }


}
