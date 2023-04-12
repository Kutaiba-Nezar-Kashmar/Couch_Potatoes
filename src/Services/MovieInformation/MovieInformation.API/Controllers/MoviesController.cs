using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Domain.Services;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("api/v1/movies")]
public class MoviesController: ControllerBase
{

    private IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [HttpGet("popular")]
    public async Task<ActionResult<JsonObject>> GetPopularMovies()
    {
        return Ok(await _movieService.GetPopularMovies());
    }
}