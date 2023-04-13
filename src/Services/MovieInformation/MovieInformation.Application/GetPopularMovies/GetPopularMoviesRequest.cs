using MediatR;

namespace MovieInformation.Application.GetPopularMovies;

public class GetPopularMoviesRequest: IRequest<GetPopularMoviesDto>
{
    public GetPopularMoviesRequest()
    {
    }
}