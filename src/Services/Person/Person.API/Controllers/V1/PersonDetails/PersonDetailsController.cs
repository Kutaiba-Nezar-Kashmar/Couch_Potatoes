using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Person.Application.FetchPersonDetails;
using Person.Infrastructure.ApiDtos;
using Person.Infrastructure.Util.Mappers;

namespace Person.API.Controllers.V1.PersonDetails;

[ApiController]
[Route("couch-potatoes/api/v1/person/details")]
public class PersonDetailsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonDetailsController> _logger;

    public PersonDetailsController
    (
        IMediator mediator,
        ILogger<PersonDetailsController> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{personId}")]
    public async Task<ActionResult<PersonDetailsDto>> GetPersonDetails(
        [FromRoute] int personId)
    {
        try
        {
            var details =
                await _mediator.Send(new PersonDetailsRequest(personId));
            var mapper = new DomainToPersonDetailsDtoMapper();

            return Ok(mapper.Map(details));
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to Retrieve person details with error: {message}",
                e.Message);
            return StatusCode((int) HttpStatusCode.InternalServerError,
                "Failed to get person details");
        }
    }
}