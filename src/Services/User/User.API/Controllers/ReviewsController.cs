using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;
using User.Application.CreateReviewForMovie;
using User.Application.GetReviewsForMovie;
using User.Domain;
using User.Domain.Exceptions;

namespace User.API.Controllers;

[ApiController]
[Route("api/v1/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ReviewsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
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
            Console.WriteLine(e);
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
        catch (Exception e) when (e is FailedToCreateReviewException or InvalidReviewException)
        {
            Console.WriteLine(e);
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), e.Message);
        }
    }
}