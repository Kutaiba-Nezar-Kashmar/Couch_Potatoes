using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetMovieCollection;
using MovieInformation.Infrastructure.ResponseDtos.MovieResponses;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/movie-collection")]
public class MovieCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<MovieCollectionsController> _logger;

    public MovieCollectionsController
    (
        IMediator mediator,
        IMapper mapper,
        ILogger<MovieCollectionsController> logger
    )
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{collectionType}")]
    public async Task<ActionResult<ReadMovieCollectionDto>> GetPopularMovies
    (
        [FromRoute] string collectionType,
        [FromQuery] int skip,
        [FromQuery] int numberOfPages
    )
    {
        try
        {
            var dto = await _mediator.Send(
                new GetMovieCollectionRequest(skip, numberOfPages,
                    collectionType));
            return Ok(_mapper.Map<ReadMovieCollectionDto>(dto));
        }
        catch (Exception e)
        {
            _logger.LogCritical(0, e, e.Message);

            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(),
                "Movie collection of type: " + collectionType + " Not found!");
        }
    }
}