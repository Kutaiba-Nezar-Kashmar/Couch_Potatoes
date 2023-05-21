using System.Net;
using System.Text.Json.Nodes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetMovieCollection;
using MovieInformation.Application.GetRecommendedMovies;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/recommended-movies")]
public class MovieRecommendationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MovieRecommendationsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{movieId:int}")]
    public async Task<ActionResult<ReadMovieCollectionDto>> GetRecommendedMovies
    (
        [FromRoute] int movieId,
        [FromQuery] int skip,
        [FromQuery] int numberOfPages
    )
    {
        try
        {
            var dto = await _mediator.Send(new GetRecommendedMoviesRequest(skip, numberOfPages, movieId));
            return Ok(_mapper.Map<ReadMovieCollectionDto>(dto));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e);
        }
    }
}