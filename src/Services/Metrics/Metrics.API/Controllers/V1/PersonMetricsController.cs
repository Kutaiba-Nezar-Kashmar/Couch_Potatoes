using System.Net;
using MediatR;
using Metrics.API.Controllers.V1.Dto;
using Metrics.Application.PersonMetrics;
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
            var dto = new PersonStatisticsDto
            {
                NumberOfMovies = stats.NumberOfMovies,
                AverageMoviesRatingsAsACast = stats.AverageMoviesRatingsAsACast,
                AverageMoviesRatingsAsACrew = stats.AverageMoviesRatingsAsACrew,
                KnownForGenre = stats.KnownForGenre
            };
            return Ok(dto);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to Retrieve statistics with error: {message}",
                e.Message);
            return StatusCode((int) HttpStatusCode.InternalServerError,
                "Failed to get statistics");
        }
    }
}