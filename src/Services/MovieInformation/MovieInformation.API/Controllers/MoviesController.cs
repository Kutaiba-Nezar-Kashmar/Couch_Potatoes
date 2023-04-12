using System.Text.Json.Nodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetPopularMovies;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("api/v1/movies")]
public class MoviesController: ControllerBase
{

    private readonly IMediator _mediator;
    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("popular")]
    public async Task<ActionResult<JsonObject>> GetPopularMovies()
    {
        var dto = await _mediator.Send(new GetPopularMoviesRequest());
        return dto.Data; // NOTE: (mibui 2023-04-12) This is not the way we want to do it, but just did it for quick test
    }
}