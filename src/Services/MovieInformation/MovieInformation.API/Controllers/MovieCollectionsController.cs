using System.Net;
using System.Text.Json.Nodes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetMovieCollection;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.Exceptions;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("api/v1/movie-collection")]
public class MovieCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MovieCollectionsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
            var dto = await _mediator.Send(new GetMovieCollectionRequest(skip, numberOfPages, collectionType));
            return Ok(_mapper.Map<ReadMovieCollectionDto>(dto));
        }
        catch (Exception e)
        {
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e);
        }
    }
}