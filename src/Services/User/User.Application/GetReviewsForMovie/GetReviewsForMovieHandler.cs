using MediatR;
using User.Application.GetReviewsForMovie.Repository;
using User.Domain;

namespace User.Application.GetReviewsForMovie;

public record GetReviewsForMovieQuery(int movieId) : IRequest<IReadOnlyCollection<Review>>;

public class GetReviewsForMovieHandler : IRequestHandler<GetReviewsForMovieQuery, IReadOnlyCollection<Review>>
{
    private readonly IGetReviewsForMovieRepository _repository;

    public GetReviewsForMovieHandler(IGetReviewsForMovieRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<Review>> Handle(GetReviewsForMovieQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.Get(request.movieId);
    }
}