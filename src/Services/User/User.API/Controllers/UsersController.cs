using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.Application;
using User.Application.AddMovieToFavorites;
using User.Application.GetReviewsForUser;
using User.Application.GetReviewsForUser.Exceptions;
using User.Application.GetUser;
using User.Application.RemoveMovieFromFavorites;
using User.Application.UpdateProfileInfo;
using User.Application.UpdateProfileInfo.Exceptions;
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
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, ILogger<UsersController> logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
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

    [HttpPut("{userId}")]
    public async Task<ActionResult<ReadUserDto>> UpdateProfileInfo([FromRoute] string userId,
        [FromBody] UpdateUserProfileInfoDto dto)
    {
        try
        {
            var updatedUser =
                await _mediator.Send(new UpdateProfileInfoCommand(userId, dto.displayName, dto.avatarUri));
            return Ok(_mapper.Map<ReadUserDto>(updatedUser));
        }
        catch (Exception e) when (e is UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(GetUser)} in {nameof(UsersController)}: {e}");
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is FailedToUpdateUserProfileInfoException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(UpdateProfileInfo)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(UpdateProfileInfo)} in {nameof(UsersController)}: {e}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ReadUserDto>>> GetUsers(
        [FromQuery] IReadOnlyCollection<string> ids)
    {
        try
        {
            var domainUsers = await _mediator.Send(new GetUsersQuery(ids));
            var usersAsDto = domainUsers.Select(user => _mapper.Map<ReadUserDto>(user));
            return Ok(usersAsDto);
        }
        catch (Exception e)
        {
            _logger.LogError(0, e, $"Failed to process {nameof(GetUser)} in {nameof(UsersController)}: {e}");
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
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(RemoveFromFavorites)}: {e}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }

    [HttpGet("{userId}/reviews")]
    public async Task<ActionResult<IReadOnlyCollection<ReadReviewDto>>> GetUserReviews([FromRoute] string userId)
    {
        try
        {
            var reviews = await _mediator.Send(new GetReviewsForUserQuery(userId));
            return Ok(reviews.Select(_mapper.Map<ReadReviewDto>));
        }
        catch (Exception e) when (e is UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e,
                $"Failed to process {nameof(RemoveFromFavorites)}: {e}");
            return NotFound(e.Message);
        }
        catch (Exception e) when (e is FailedToRetrieveReviewsForUserException)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(RemoveFromFavorites)}: {e}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(RemoveFromFavorites)}: {e}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }
}