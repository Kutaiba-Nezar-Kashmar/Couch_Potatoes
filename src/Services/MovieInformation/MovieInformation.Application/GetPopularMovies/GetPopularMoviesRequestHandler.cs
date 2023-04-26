using System.Runtime.CompilerServices;
using MediatR;
using MovieInformation.Application.GetPopularMovies.Repositories;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetPopularMovies;

public record GetPopularMoviesRequest
(
    int Skip,
    int NumberOfPages
) : IRequest<MovieCollection>;

public class GetPopularMoviesRequestHandler : IRequestHandler<GetPopularMoviesRequest, MovieCollection>
{
    private readonly IPopularMovieRepository _popularMovieRepository;

    public GetPopularMoviesRequestHandler(IPopularMovieRepository popularMovieRepository)
    {
        _popularMovieRepository = popularMovieRepository;
    }

    public async Task<MovieCollection> Handle(GetPopularMoviesRequest request,
        CancellationToken cancellationToken)
    {
        List<Task<MovieCollectionPage>> getMoviesRequests = new();
        for (int i = 0; i < request.NumberOfPages; i++)
        {
            getMoviesRequests.Add(_popularMovieRepository.GetPopularMovies(ResolvePage(request.Skip, i)));
        }

        return new MovieCollection
        {
            pages = await Task.WhenAll(getMoviesRequests)
        };
    }
    private int ResolvePage(int skip, int page) => skip + page;
}