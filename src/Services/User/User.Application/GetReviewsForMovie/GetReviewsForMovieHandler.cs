using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.GetReviewsForMovie.Repository;
using User.Domain;

namespace User.Application.GetReviewsForMovie;

public record GetReviewsForMovieQuery(int movieId) : IRequest<IReadOnlyCollection<Review>>;

public class GetReviewsForMovieHandler : IRequestHandler<GetReviewsForMovieQuery, IReadOnlyCollection<Review>>
{
    private readonly IGetReviewsForMovieRepository _repository;
    private readonly ILogger _logger;

    public GetReviewsForMovieHandler(IGetReviewsForMovieRepository repository, ILogger<GetReviewsForMovieHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Review>> Handle(GetReviewsForMovieQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.Get(request.movieId);
    }
}