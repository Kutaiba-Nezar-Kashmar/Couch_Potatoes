using MediatR;
using Microsoft.Extensions.Logging;
using Search.Application.MultiSearch.Exceptions;
using Search.Application.MultiSearch.Repositories;
using Search.Domain.Models;

namespace Search.Application.MultiSearch;

public record MultiSearchRequest(string query) : IRequest<MultiSearchResponse>;

public class
    MultiSearchHandler : IRequestHandler<MultiSearchRequest,
        MultiSearchResponse>
{
    private readonly ILogger<MultiSearchHandler> _logger;
    private readonly IMultiSearchRepository _multiSearchRepository;

    public MultiSearchHandler
    (
        ILogger<MultiSearchHandler> logger,
        IMultiSearchRepository multiSearchRepository
    )
    {
        _logger = logger;
        _multiSearchRepository = multiSearchRepository;
    }

    public async Task<MultiSearchResponse> Handle
    (
        MultiSearchRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation("Handling multi search");
            return await _multiSearchRepository.MultiSearch(
                request.query);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to performing multi search on {Date} with error: {EMessage} {ex}",
                DateTimeOffset.UtcNow,
                e.Message,
                e);
            throw new CannotMultiSearchException(
                "Something went wrong when performing multi search", e);
        }
    }
}