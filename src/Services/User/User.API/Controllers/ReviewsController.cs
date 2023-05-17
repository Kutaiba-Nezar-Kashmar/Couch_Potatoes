using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.Application.CreateReviewForMovie;
using User.Application.CreateReviewForMovie.Exceptions;
using User.Application.GetReviewsForMovie;
using User.Application.UpvoteReview;
using User.Domain;
using User.Domain.Exceptions;

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
    public async Task<ActionResult<IReadOnlyCollection<ReviewDto>>> Get([FromRoute] int movieId)
    {
        try
        {
            var reviews = await _mediator.Send(new GetReviewsForMovieQuery(movieId));
            return Ok(reviews
                .Select(r => _mapper.Map<ReviewDto>(r))
                .ToList()
            );
        }
        catch (Exception e)
        {
            _logger.LogError(0, e, $"Failed to process {nameof(Get)}");
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
            _logger.LogError(0, e.InnerException ?? e, $"Failed to process {nameof(Create)}: {e}");
            return Conflict(e.Message);
        }
        catch (Exception e) when (e is FailedToCreateReviewException or InvalidReviewException)
        {
            _logger.LogError(0, e.InnerException ?? e, $"Failed to process {nameof(Create)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
    }

    [HttpPost("{movieId}/{reviewId}/votes")]
    public async Task<ActionResult> Vote([FromRoute] int movieId, [FromRoute] Guid reviewId,
        [FromBody]
        VoteReviewDto dto)
    {
        try
        {
            await _mediator.Send(new VoteReviewCommand(dto.UserId, movieId, reviewId, dto.Direction.ToVoteDirection()));
            return Ok();
        }
        catch (Exception e) when (e is UserDoesNotExistException or ReviewDoesNotExistException)
        {
            _logger.LogError(0, e, $"Review or user did not exist: {e.InnerException ?? e}");
            return StatusCode(HttpStatusCode.NotFound.Cast<int>(), e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(0, e, $"Failed to process {nameof(Vote)} in {nameof(ReviewsController)}");
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>());
        }
    }
}