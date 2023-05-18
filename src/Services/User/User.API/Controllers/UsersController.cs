using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.Application.AddMovieToFavorites;
using User.Application.GetUser;
using User.Application.RemoveMovieFromFavorites;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CouchPotatoUser>> GetUser([FromRoute] string userId)
    {
        try
        {
            var user = await _mediator.Send(new GetUserQuery(userId));
            return Ok(user);
        }
        catch (Exception e) when (e is UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(GetUser)} in {nameof(UsersController)}: {e}");
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(GetUser)} in {nameof(UsersController)}: {e}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }

    [HttpPost("{userId}/favorites")]
    public async Task<ActionResult> AddMovieToFavorite([FromRoute] string userId, [FromBody] AddMovieToFavoriteDto dto)
    {
        try
        {
            await _mediator.Send(new AddMovieToFavoritesCommand(userId, dto.MovieId));
            return Ok();
        }
        catch (FailedToCreateReviewException e)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(AddMovieToFavorite)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(AddMovieToFavorite)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }

    [HttpDelete("{userId}/favorites/{movieId}")]
    public async Task<ActionResult> RemoveFromFavorites([FromRoute] string userId, [FromRoute] int movieId)
    {
        try
        {
            await _mediator.Send(new RemoveMovieFromFavoritesCommand(userId, movieId));
            return Ok();
        }
        catch (Exception e) when (e is UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(RemoveFromFavorites)}: {e}");
            return NotFound(e.Message)
                ;
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(RemoveFromFavorites)}: {e}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }
}