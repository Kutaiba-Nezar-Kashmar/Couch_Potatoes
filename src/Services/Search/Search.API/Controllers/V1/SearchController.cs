using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.MultiSearch;
using Search.Domain.Models;
using Search.Infrastructure.ControllerDtos;
using Search.Infrastructure.Util.Mappers;

namespace Search.API.Controllers.V1;

[ApiController]
[Route("couch-potatoes/api/v1/search")]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SearchController> _logger;
    private readonly IMapper _mapper;

    public SearchController
    (
        IMediator mediator,
        ILogger<SearchController> logger,
        IMapper mapper
    )
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("multi")]
    public async Task<ActionResult<MultiSearchResponseDto>> MultiSearch
    (
        [FromQuery] string query
    )
    {
        try
        {
            if (string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query))
                return Ok(new MultiSearchResponse
                {
                    Movies = new List<MovieSearch>(),
                    People = new List<PersonSearch>()
                });
            var result =
                await _mediator.Send(new MultiSearchRequest(query));
            var mapper = new MultiSearchToControllerDtoMapper(_mapper);
            return Ok(mapper.Map(result));
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to perform multi search with error: {Message}",
                e.Message);
            return StatusCode((int) HttpStatusCode.InternalServerError,
                "Failed to perform multi search");
        }
    }
}