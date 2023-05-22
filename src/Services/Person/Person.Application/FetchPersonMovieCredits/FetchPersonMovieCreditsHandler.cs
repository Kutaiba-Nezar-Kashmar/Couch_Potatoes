using MediatR;
using Microsoft.Extensions.Logging;
using Person.Application.FetchPersonMovieCredits.Exceptions;
using Person.Application.FetchPersonMovieCredits.Repositories;
using Person.Domain.Models.Person;

namespace Person.Application.FetchPersonMovieCredits;

public record PersonMovieCreditsHandlerRequest
    (int PersonId) : IRequest<PersonMovieCredits>;

public class FetchPersonMovieCreditsHandler : IRequestHandler<
    PersonMovieCreditsHandlerRequest,
    PersonMovieCredits>
{
    private readonly IFetchPersonMovieCreditsRepository
        _fetchPersonMovieCreditsRepository;

    private readonly ILogger<FetchPersonMovieCreditsHandler> _logger;

    public FetchPersonMovieCreditsHandler
    (
        IFetchPersonMovieCreditsRepository fetchPersonMovieCreditsRepository,
        ILogger<FetchPersonMovieCreditsHandler> logger
    )
    {
        _fetchPersonMovieCreditsRepository = fetchPersonMovieCreditsRepository;
        _logger = logger;
    }

    public async Task<PersonMovieCredits> Handle
    (
        PersonMovieCreditsHandlerRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation("Handling fetch person movie credits");
            var credits =
                await _fetchPersonMovieCreditsRepository
                    .FetchPersonMovieCreditsByPersonId(request.PersonId);
            return credits;
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                "Fetching person movie credits on {UtcNow} could not be done: {E}",
                DateTimeOffset.UtcNow, e);
            throw new CannotFetchPersonMovieCredits(
                "Something went wrong when fetching credits", e);
        }
    }
}