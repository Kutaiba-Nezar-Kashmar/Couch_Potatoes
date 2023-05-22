using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Person.Application.FetchPersonMovieCredits;
using Person.Infrastructure.ApiDtos;
using Person.Infrastructure.Util.Mappers;

namespace Person.API.Controllers.V1.PersonMovieCredits;

[ApiController]
[Route("couch-potatoes/api/v1/person/movie-credits")]
public class PersonMovieCreditsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonMovieCreditsController> _logger;

    public PersonMovieCreditsController
    (
        IMediator mediator,
        ILogger<PersonMovieCreditsController> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{personId}")]
    public async Task<ActionResult<PersonMovieCreditsDto>>
        GetPersonMovieCredits([FromRoute] int personId)
    {
        try
        {
            _logger.LogInformation("GetPersonMovieCredits endpoint called");
            var credits =
                await _mediator.Send(
                    new PersonMovieCreditsHandlerRequest(personId));
            var mapper = new DomainToPersonMovieCreditDtoMapper();
            var response = mapper.Map(credits);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to Retrieve person movie credits with error: {message}",
                e.Message);
            return StatusCode((int) HttpStatusCode.InternalServerError,
                "Failed to get person movie credits");
        }
    }
}