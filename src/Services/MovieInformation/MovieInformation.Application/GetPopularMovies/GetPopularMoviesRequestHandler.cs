using MediatR;
using MovieInformation.Application.GetPopularMovies.Repositories;

namespace MovieInformation.Application.GetPopularMovies;

public class GetPopularMoviesRequestHandler: IRequestHandler<GetPopularMoviesRequest, GetPopularMoviesDto>
{
    private readonly IPopularMovieRepository _popularMovieRepository;

    public GetPopularMoviesRequestHandler(IPopularMovieRepository popularMovieRepository)
    {
        _popularMovieRepository = popularMovieRepository;
    }
    
    public async Task<GetPopularMoviesDto> Handle(GetPopularMoviesRequest request, CancellationToken cancellationToken)
    {
        return new GetPopularMoviesDto
        {
            Data = await _popularMovieRepository.GetPopularMovies()
        };
    }
}