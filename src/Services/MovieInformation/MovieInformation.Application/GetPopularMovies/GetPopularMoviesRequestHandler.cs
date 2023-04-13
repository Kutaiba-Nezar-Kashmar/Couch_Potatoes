using MediatR;
using MovieInformation.Domain.Repositories;
using MovieInformation.Infrastructure.Repositories;

namespace MovieInformation.Application.GetPopularMovies;

public class GetPopularMoviesRequestHandler: IRequestHandler<GetPopularMoviesRequest, GetPopularMoviesDto>
{
    private readonly IMovieRepository _movieRepository;

    public GetPopularMoviesRequestHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository?? new TmdbMovieRepository();
    }
    
    public async Task<GetPopularMoviesDto> Handle(GetPopularMoviesRequest request, CancellationToken cancellationToken)
    {
        return new GetPopularMoviesDto
        {
            Data = await _movieRepository.GetPopularMovies()
        };
    }
}