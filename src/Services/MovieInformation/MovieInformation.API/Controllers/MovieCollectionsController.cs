using System.Text.Json.Nodes;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetPopularMovies;
using MovieInformation.Domain.Models;
using MovieInformation.Infrastructure.ResponseDtos;

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

    [HttpGet("popular")]
    public async Task<ActionResult<ReadMovieCollectionDto>> GetPopularMovies([FromQuery] int skip,
        [FromQuery] int numberOfPages)
    {
        var dto = await _mediator.Send(new GetPopularMoviesRequest(skip, numberOfPages));
        return Ok(_mapper.Map<ReadMovieCollectionDto>(dto)); 
    }
}