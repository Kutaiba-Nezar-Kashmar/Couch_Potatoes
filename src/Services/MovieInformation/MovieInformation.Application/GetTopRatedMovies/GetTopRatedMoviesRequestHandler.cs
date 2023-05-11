using MediatR;
using Microsoft.Extensions.Logging;
using MovieInformation.Domain.Models;

namespace MovieInformation.Application.GetTopRatedMovies;

public record GetTopRatedMoviesRequest
(
    int Skip,
    int NumberOfPages
) : IRequest<MovieCollection>;

public class GetTopRatedMoviesRequestHandler : IRequestHandler<GetTopRatedMoviesRequest, MovieCollection>
{
    
    public Task<MovieCollection> Handle(GetTopRatedMoviesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}