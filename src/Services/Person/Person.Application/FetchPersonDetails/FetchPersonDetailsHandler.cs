using MediatR;
using Microsoft.Extensions.Logging;
using Person.Application.FetchPersonDetails.Exceptions;
using Person.Application.FetchPersonDetails.Repositories;
using Person.Domain.Models.Person;

namespace Person.Application.FetchPersonDetails;

public record PersonDetailsRequest(int PersonId) : IRequest<PersonDetails>;

public class
    FetchPersonDetailsHandler : IRequestHandler<PersonDetailsRequest,
        PersonDetails>
{
    private readonly IFetchPersonDetailsRepository
        _fetchPersonDetailsRepository;

    private readonly ILogger<FetchPersonDetailsHandler> _logger;

    public FetchPersonDetailsHandler
    (
        IFetchPersonDetailsRepository fetchPersonDetailsRepository,
        ILogger<FetchPersonDetailsHandler> logger
    )
    {
        _fetchPersonDetailsRepository = fetchPersonDetailsRepository;
        _logger = logger;
    }

    public async Task<PersonDetails> Handle
    (
        PersonDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation("Fetching person details");
            return await _fetchPersonDetailsRepository.FetchPersonDetailsById(
                request.PersonId);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Failed to fetch person details on {Date} with error: {EMessage} {ex}",
                DateTimeOffset.UtcNow,
                e.Message,
                e);
            throw new CannotFetchPersonDetailsException(
                "Something went wrong when fetching person details", e);
        }
    }
}