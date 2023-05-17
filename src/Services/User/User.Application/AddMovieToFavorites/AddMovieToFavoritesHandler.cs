using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.AddMovieToFavorites.Exceptions;
using User.Application.AddMovieToFavorites.Repository;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.AddMovieToFavorites;

public record AddMovieToFavoritesCommand(string userId, int movieId) : IRequest;

public class AddMovieToFavoritesHandler : IRequestHandler<AddMovieToFavoritesCommand>
{
    private readonly IAddMovieToFavoritesRepository _repository;
    private readonly IAuthenticationRepository _auth;
    private readonly ILogger _logger;

    public AddMovieToFavoritesHandler(IAddMovieToFavoritesRepository repository, IAuthenticationRepository auth,
        ILogger<AddMovieToFavoritesHandler> logger)
    {
        _repository = repository;
        _auth = auth;
        _logger = logger;
    }

    public async Task Handle(AddMovieToFavoritesCommand request, CancellationToken cancellationToken)
    {
        var user = await _auth.GetUserById(request.userId);
        if (user is null)
        {
            throw new UserDoesNotExistException(request.userId);
        }

        try
        {
            await _repository.AddMovieToUsersFavorites(user, request.movieId);
        }
        catch (Exception e)
        {
            _logger.LogError(1, e, $"Failed to process {nameof(Handle)} in {nameof(AddMovieToFavoritesHandler)}");
            throw new FailedToAddMovieToUserFavoritesException(request.userId, request.movieId, e);
        }
    }
}