using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.Application.CreateReviewForMovie;
using User.Application.CreateReviewForMovie.Exceptions;
using User.Application.DeleteReview;
using User.Application.GetReviewsForMovie;
using User.Application.UpdateReviewForMovie;
using User.Application.UpvoteReview;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReviewsController(IMapper mapper, IMediator mediator, ILogger<ReviewsController> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{movieId}")]
    public async Task<ActionResult<IReadOnlyCollection<ReadReviewDto>>> Get([FromRoute] int movieId)
    {
        try
        {
            var reviews = await _mediator.Send(new GetReviewsForMovieQuery(movieId));
            return Ok(reviews
                .Select(r => _mapper.Map<ReadReviewDto>(r))
                .ToList()
            );
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(Get)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(),
                $"Failed to get reviews for movie with id {movieId}");
        }
    }

    [HttpPost("{movieId}")]
    public async Task<ActionResult> Create([FromRoute] int movieId, [FromBody] CreateReviewDto reviewDto)
    {
        try
        {
            await _mediator.Send(new CreateReviewForMovieCommand(movieId, reviewDto.UserId, reviewDto.Rating,
                reviewDto.ReviewText));
            return Ok();
        }
        catch (Exception e) when (e is UserHasExistingReviewException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e, $"Failed to process {nameof(Create)}: {e}");
            return Conflict(e.Message);
        }
        catch (Exception e) when (e is FailedToCreateReviewException or InvalidReviewException)
        {
            _logger.LogError(LogEvent.Api, e.InnerException ?? e, $"Failed to process {nameof(Create)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
    }

    [HttpPut("{movieId}/{reviewId}")]
    public async Task<ActionResult<Review>> Update([FromRoute] int movieId, [FromRoute] Guid reviewId,
        [FromBody] UpdateReviewDto dto)
    {
        try
        {
            var updatedReview =
                await _mediator.Send(new UpdateReviewForMovieCommand(dto.UserId, movieId, reviewId, dto.Rating,
                    dto.ReviewText));

            return Ok(_mapper.Map<ReadReviewDto>(updatedReview));
        }
        catch (Exception e) when (e is ReviewDoesNotExistException or UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e, $"Review or user did not exist: {e.InnerException ?? e}");
            return StatusCode(HttpStatusCode.NotFound.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(Update)} in {nameof(ReviewsController)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }

    [HttpDelete("{movieId}/{reviewId}")]
    public async Task<ActionResult> Delete([FromRoute] int movieId, [FromRoute] Guid reviewId,
        [FromBody] DeleteReviewDto dto)
    {
        try
        {
            await _mediator.Send(new DeleteReviewForMovieCommand(dto.UserId, movieId, reviewId));
            return Ok();
        }
        catch (Exception e) when (e is ReviewDoesNotExistException or UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e, $"Review or user did not exist: {e.InnerException ?? e}");
            return StatusCode(HttpStatusCode.NotFound.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(Delete)} in {nameof(ReviewsController)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }

    [HttpPost("{movieId}/{reviewId}/votes")]
    public async Task<ActionResult> Vote([FromRoute] int movieId, [FromRoute] Guid reviewId,
        [FromBody] VoteReviewDto dto)
    {
        try
        {
            await _mediator.Send(new VoteReviewCommand(dto.UserId, movieId, reviewId, dto.Direction.ToVoteDirection()));
            return Ok();
        }
        catch (Exception e) when (e is UserDoesNotExistException or ReviewDoesNotExistException)
        {
            _logger.LogError(LogEvent.Api, e, $"Review or user did not exist: {e.InnerException ?? e}");
            return StatusCode(HttpStatusCode.NotFound.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(LogEvent.Api, e, $"Failed to process {nameof(Vote)} in {nameof(ReviewsController)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }
}