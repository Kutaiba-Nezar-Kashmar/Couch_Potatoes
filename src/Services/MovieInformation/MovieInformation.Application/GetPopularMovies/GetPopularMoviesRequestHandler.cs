using MediatR;
using MovieInformation.Application.GetPopularMovies.Repositories;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetPopularMovies;

public record GetPopularMoviesRequest
(
    int Skip,
    int NumberOfPages
) : IRequest<IReadOnlyCollection<Movie>>;


public class GetPopularMoviesRequestHandler : IRequestHandler<GetPopularMoviesRequest, IReadOnlyCollection<Movie>>
{
    private readonly IPopularMovieRepository _popularMovieRepository;

    public GetPopularMoviesRequestHandler(IPopularMovieRepository popularMovieRepository)
    {
        _popularMovieRepository = popularMovieRepository;
    }

    public async Task<IReadOnlyCollection<Movie>> Handle(GetPopularMoviesRequest request, CancellationToken cancellationToken)
    {
        return await _popularMovieRepository.GetPopularMovies(request.Skip, request.NumberOfPages);

    }
}