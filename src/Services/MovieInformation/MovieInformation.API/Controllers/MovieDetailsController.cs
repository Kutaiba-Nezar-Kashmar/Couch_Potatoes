using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieInformation.Application.GetMovie;
using MovieInformation.Infrastructure.ResponseDtos;
using MovieInformation.Infrastructure.Util;

namespace MovieInformation.API.Controllers;

[ApiController]
[Route("couch-potatoes/api/v1/movie-details")]
public class MovieDetailsController: ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;


    public MovieDetailsController(IMediator mediator, IMapper mapper, ILogger<MovieDetailsController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    
    
    [HttpGet("{movieId:int}")]
    public async Task<ActionResult<ReadDetailedMovieDto>> GetMovieDetails
    (
        [FromRoute] int movieId
    )
    {
        try
        {
            var dto = await _mediator.Send(new GetMovieDetailsRequest(movieId));
            return Ok(_mapper.Map<ReadDetailedMovieDto>(dto));
        }
        catch (Exception e)
        {
            _logger.LogCritical(0,e, e.Message);
            
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), "Movie with movieId: "+movieId+" Not found!");
        }
    }
}