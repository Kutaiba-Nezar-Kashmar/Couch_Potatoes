using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Person.API.Controllers.V1.PersonDetails.Dto;
using Person.Application.FetchPersonDetails;

namespace Person.API.Controllers.V1.PersonDetails;

[ApiController]
[Route("api/v1/person/details")]
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
            var dto = new PersonDetailsDto
            {
                IsAdult = details.IsAdult,
                Aliases = details.Aliases,
                Biography = details.Biography,
                Birthday = details.Birthday,
                DeathDay = details.DeathDay,
                Gender = Enum.GetName(details.Gender)!,
                Homepage = details.Homepage,
                KnownForDepartment = details.KnownForDepartment,
                Name = details.Name,
                PlaceOfBirth = details.PlaceOfBirth,
                Popularity = details.Popularity,
                ProfilePath = details.ProfilePath
            };

            return Ok(dto);
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