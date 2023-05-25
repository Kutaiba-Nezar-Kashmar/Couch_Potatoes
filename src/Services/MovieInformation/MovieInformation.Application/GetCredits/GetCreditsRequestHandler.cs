using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Application.GetCredits.Repositories;
using MovieInformation.Application.GetMovie;
using MovieInformation.Application.GetMovie.Exceptions;
using MovieInformation.Domain.Models.Person;

namespace MovieInformation.Application.GetCredits;

public record GetCreditsRequest
(
    int MovieId
) : IRequest<PersonMovieCredits>;

public class
    GetCreditsRequestHandler : IRequestHandler<GetCreditsRequest,
        PersonMovieCredits>
{
    private readonly IGetCreditsRepository _getCreditsRepository;
    private readonly ILogger<GetCreditsRequestHandler> _logger;

    public GetCreditsRequestHandler(IGetCreditsRepository getCreditsRepository,
        ILogger<GetCreditsRequestHandler> logger)
    {
        _getCreditsRepository = getCreditsRepository;
        _logger = logger;
    }

    public async Task<PersonMovieCredits> Handle(GetCreditsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var getCreditsRequest =
                await _getCreditsRepository.GetMovieCredits(request.MovieId);

            return getCreditsRequest;
        }
        catch (Exception e)
        {
            _logger.LogError("{MovieDetailsRequestHandlerName}: ${EMessage}",
                nameof(GetMovieDetailsRequestHandler), e.Message);
            throw new FailedToGetMovieDetailsException(
                $"Failed to retrieve movie credits with movieId:${request.MovieId}");
        }
    }
}