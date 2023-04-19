using System.Text.Json.Nodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetPopularMovies;
using MovieInformation.Domain.Models;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("api/v1/movies")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IReadOnlyCollection<Movie>>> GetPopularMovies([FromQuery] int skip,
        [FromQuery] int numberOfPages)
    {
        var dto = await _mediator.Send(new GetPopularMoviesRequest(skip, numberOfPages));
        return Ok(dto); // NOTE: (mibui 2023-04-12) This is not the way we want to do it, but just did it for quick test
    }
}