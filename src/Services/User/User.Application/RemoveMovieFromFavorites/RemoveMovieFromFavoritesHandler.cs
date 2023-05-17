using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.AddMovieToFavorites.Exceptions;
using User.Application.RemoveMovieFromFavorites.Exceptions;
using User.Application.RemoveMovieFromFavorites.Repository;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.RemoveMovieFromFavorites;

public record RemoveMovieFromFavoritesCommand(string userId, int movieId) : IRequest;

public class RemoveMovieFromFavoritesHandler : IRequestHandler<RemoveMovieFromFavoritesCommand>
{
    private readonly IRemoveMovieFromFavoritesRepository _repository;
    private readonly IAuthenticationRepository _auth;
    private readonly ILogger _logger;

    public RemoveMovieFromFavoritesHandler(IRemoveMovieFromFavoritesRepository repository,
        IAuthenticationRepository auth, ILogger<RemoveMovieFromFavoritesHandler> logger)
    {
        _auth = auth;
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(RemoveMovieFromFavoritesCommand request, CancellationToken cancellationToken)
    {
        var user = await _auth.GetUserById(request.userId);
        if (user is null)
        {
            throw new UserDoesNotExistException(request.userId);
        }

        try
        {
            await _repository.RemoveMovieFromFavoritesForUser(user, request.movieId);
        }
        catch (Exception e)
        {
            _logger.LogError(1, e, $"Failed to process {nameof(Handle)} in {nameof(RemoveMovieFromFavoritesHandler)}");
            throw new FailedToRemoveMovieFromFavoritesException(request.userId, request.movieId, e);
        }
    }
}