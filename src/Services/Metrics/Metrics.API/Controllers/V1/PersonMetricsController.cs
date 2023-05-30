using System.Net;
using MediatR;
using Metrics.Application.PersonMetrics;
using Metrics.Infrastructure.ControllerDtos;
using Metrics.Infrastructure.Util.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.API.Controllers.V1;

[ApiController]
[Route("couch-potatoes/api/v1/person/metrics")]
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
    public async Task<ActionResult<PersonStatisticsDto>>
        GetPersonMovieStatistics([FromRoute] int personId)
    {
        try
        {
            var stats =
                await _mediator.Send(new PersonMetricHandlerRequest(personId));
            var mapper = new DomainToPersonStatisticsDtoMapper();
            return Ok(mapper.Map(stats));
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to Retrieve statistics with error: {Message}",
                e.Message);
            return StatusCode((int) HttpStatusCode.InternalServerError,
                "Failed to get statistics");
        }
    }
}
