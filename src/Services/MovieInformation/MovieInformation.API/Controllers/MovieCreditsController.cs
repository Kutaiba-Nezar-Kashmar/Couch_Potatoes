using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetCredits;
using MovieInformation.Domain.Models.Person;
using MovieInformation.Infrastructure.ControllerDtos.Person;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/movie-credits")]
public class MovieCreditsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieCreditsController> _logger;


    public MovieCreditsController
    (
        IMediator mediator,
        IMapper mapper,
        ILogger<MovieCreditsController> logger
    )
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{movieId:int}")]
    public async Task<ActionResult<PersonMovieCredits>> GetMovieCredits
    (
        [FromRoute] int movieId
    )
    {
        try
        {
            var dto = await _mediator.Send(new GetCreditsRequest(movieId));
            return Ok(_mapper.Map<PersonMovieCreditsDto>(dto));
        }
        catch (Exception e)
        {
            _logger.LogCritical(0, e, e.Message);

            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(),
                "Movie credits for movie with movieId: " + movieId +
                " Not found!");
        }
    }
}