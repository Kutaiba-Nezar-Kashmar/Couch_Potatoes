using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.Application.AddMovieToFavorites;
using User.Domain;
using User.Domain.Exceptions;

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
            _logger.LogError(0, e.InnerException ?? e, $"Failed to process {nameof(AddMovieToFavorite)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(0, e, $"Failed to process {nameof(AddMovieToFavorite)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }
}
