using EventService.Application.FetchEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers;

[ApiController]
[Route("api")]
public class TestController : ControllerBase
{
    private IMediator _mediator;
    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> Testing([FromQuery] string name)
    {
        var s = await _mediator.Send(new FetchEventsQuery(name));
        return Ok(s);
    }
}