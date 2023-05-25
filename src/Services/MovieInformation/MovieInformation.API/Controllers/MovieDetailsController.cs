using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetMovie;
using MovieInformation.Application.GetMovies;
using MovieInformation.Application.GetMovies.Exceptions;
using MovieInformation.Infrastructure.ControllerDtos.Movie;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/movie-details")]
public class MovieDetailsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieDetailsController> _logger;


    public MovieDetailsController
    (
        IMediator mediator,
        IMapper mapper,
        ILogger<MovieDetailsController> logger
    )
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }


    [HttpGet("{movieId:int}")]
    public async Task<ActionResult<MovieControllerDto>> GetMovieDetails
    (
        [FromRoute] int movieId
    )
    {
        try
        {
            var dto = await _mediator.Send(new GetMovieDetailsRequest(movieId));
            var mapper = new DomainToReadDetailedMovieDtoMapper();
            return Ok(mapper.Map(dto));
        }
        catch (Exception e)
        {
            _logger.LogCritical(0, e, e.Message);

            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(),
                "Movie with movieId: " + movieId + " Not found!");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<MovieControllerDto>>>
        GetMovies(
            [FromQuery] IReadOnlyCollection<int> ids)
    {
        if (ids == null || !ids.Any())
        {
            return BadRequest("No movie ids were provided");
        }

        try
        {
            var domainMovies = await _mediator.Send(new GetMoviesQuery(ids));
            var moviesAsDtos = domainMovies
                .Select(_mapper.Map<MovieControllerDto>).ToList();
            return Ok(moviesAsDtos);
        }
        catch (Exception e) when (e is FailedToGetMoviesException)
        {
            _logger.LogError(2, e,
                "Failed to process {MoviesName} in {MovieDetailsControllerName}: {E}",
                nameof(GetMovies), nameof(MovieDetailsController), e);
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(),
                e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(2, e,
                "Failed to process {MoviesName} in {MovieDetailsControllerName}: {E}",
                nameof(GetMovies), nameof(MovieDetailsController), e);
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(),
                e.Message);
        }
    }
}