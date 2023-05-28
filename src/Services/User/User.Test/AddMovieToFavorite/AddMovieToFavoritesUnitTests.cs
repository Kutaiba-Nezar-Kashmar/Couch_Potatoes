using Microsoft.Extensions.Logging;
using Moq;
using User.Application.AddMovieToFavorites;
using User.Application.AddMovieToFavorites.Exceptions;
using User.Application.AddMovieToFavorites.Repository;
using User.Application.UpvoteReview;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Test;

[TestFixture]
public class AddMovieToFavoritesUnitTests
{
    private readonly Mock<IAuthenticationRepository> _authMock = new();
    private readonly Mock<IAddMovieToFavoritesRepository> _repositoryMock = new();
    private readonly Mock<ILogger<AddMovieToFavoritesHandler>> _loggerMock = new();

    [Test]
    public void AddMovieToFavorites_UserDoesNotExists_ThrowsUserDoesNotExistException()
    {
        // Arrange
        var nonExistingUserId = "kdsmakdlsmalkdmaskldmsaklmdsa";
        CouchPotatoUser nullUser = null;

        var handler = CreateHandler(
            null,
            auth => { auth.Setup(x => x.GetUserById(nonExistingUserId)).ReturnsAsync(nullUser); }
            );

        AddMovieToFavoritesCommand testCommand = new(nonExistingUserId, 550);

        // Act & Assert
        Assert.ThrowsAsync<UserDoesNotExistException>(() => handler.Handle(testCommand, new CancellationToken()));
    }

    [Test]
    public void AddMovieToFavorites_ErrorInRepository_ThrowsFailedToAddMovieToFavoriteException()
    {
        // Arrange
        CouchPotatoUser existingUser = TestUtil.CreateGenericUser();
        int movieId = 550;

        var handler = CreateHandler(
            (repository) =>
            {
                repository.Setup(x => x.AddMovieToUsersFavorites(existingUser, movieId))
                    .ThrowsAsync(new Exception("Something went wrong..."));
            },
            (auth) =>
            {
                auth.Setup(x => x.GetUserById(existingUser.Id))
                    .ReturnsAsync(existingUser);
            }
        );

        AddMovieToFavoritesCommand testCommand = new(existingUser.Id, 550);

        // Act & Assert
        Assert.ThrowsAsync<FailedToAddMovieToUserFavoritesException>(() =>
            handler.Handle(testCommand, new CancellationToken()));
    }


    private AddMovieToFavoritesHandler CreateHandler(
        Action<Mock<IAddMovieToFavoritesRepository>>? configureRepository = null,
        Action<Mock<IAuthenticationRepository>>? configureAuth = null)
    {
        configureRepository?.Invoke(_repositoryMock);
        configureAuth?.Invoke(_authMock);

        return new AddMovieToFavoritesHandler(_repositoryMock.Object, _authMock.Object, _loggerMock.Object);
    }
}