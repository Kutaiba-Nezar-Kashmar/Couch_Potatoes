using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.MultiSearch;
using Search.Domain.Models;

namespace Search.API.Controllers.V1;

[ApiController]
[Route("couch-potatoes/api/v1/search")]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SearchController> _logger;

    public SearchController
    (
        IMediator mediator,
        ILogger<SearchController> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("/multi")]
    public async Task<ActionResult<MultiSearchResponse>> MultiSearch(
        [FromQuery] string query)
    {
        try
        {
            var result = await _mediator.Send(new MultiSearchRequest(query));
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to perform multi search with error: {message}",
                e.Message);
            return StatusCode((int) HttpStatusCode.InternalServerError,
                "Failed to perform multi search");
        }
    }
}