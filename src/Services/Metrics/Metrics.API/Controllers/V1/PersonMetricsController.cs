using System.Net;
using MediatR;
using Metrics.API.Controllers.V1.Dto;
using Metrics.Application.PersonMetrics;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.API.Controllers.V1;

[ApiController]
[Route("api/v1/person_metrics")]
public class PersonMetricsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonMetricsController> _logger;

    public PersonMetricsController
    (
        IMediator mediator,
        ILogger<PersonMetricsController> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet("{personId}")]
    public async Task<ActionResult<PersonStatisticsDto>> GetPopularMovies([FromRoute] int personId)
    {
        try
        {
            var dto = await _mediator.Send(new PersonMetricHandlerRequest(personId));
            return Ok(dto);
        }
        catch (Exception e)
        {
            return StatusCode((int) HttpStatusCode.InternalServerError, e);
        }
    }
}