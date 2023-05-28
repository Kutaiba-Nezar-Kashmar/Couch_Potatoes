using Microsoft.Extensions.Logging;
using Moq;
using User.Application.AddMovieToFavorites;
using User.Application.AddMovieToFavorites.Repository;
using User.Application.CreateReviewForMovie;
using User.Application.CreateReviewForMovie.Exceptions;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Test.CreateReview;

[TestFixture]
public class CreateReviewUnitTests
{
    private readonly Mock<IAuthenticationRepository> _authMock = new();
    private readonly Mock<ICreateReviewForMovieRepository> _repositoryMock = new();
    private readonly Mock<ILogger<CreateReviewForMovieHandler>> _loggerMock = new();


    [Test]
    public void CreateReview_UserDoesNotExist_ThrowsUserDoesNotExistException()
    {
        // Arrange
        var nonExistingUserId = "klsadnmdklmsakldmklj";
        var movieId = 550;
        CouchPotatoUser nullUser = null;

        var handler = CreateHandler(
            null,
            auth => { auth.Setup(x => x.GetUserById(nonExistingUserId)).ReturnsAsync(nullUser); }
        );

        CreateReviewForMovieCommand testCommand = new(movieId, nonExistingUserId, 5, "meh");

        // Assert & act
        Assert.ThrowsAsync<UserDoesNotExistException>(() => handler.Handle(testCommand, new CancellationToken()));
    }

    [Test]
    [TestCase(-1)]
    [TestCase(-10)]
    [TestCase(11)]
    [TestCase(100)]
    public void CreateReview_InvalidRating_ThrowsInvalidReviewException(int rating)
    {
        // Arrange
        var movieId = 550;
        var existingUser = TestUtil.CreateGenericUser();

        var handler = CreateHandler(
            null,
            auth => { auth.Setup(x => x.GetUserById(existingUser.Id)).ReturnsAsync(existingUser); }
        );

        CreateReviewForMovieCommand testCommand = new(movieId,existingUser.Id , rating, "meh");

        // Assert & act
        Assert.ThrowsAsync<InvalidReviewException>(() => handler.Handle(testCommand, new CancellationToken()));
    }

    [Test]
    public void CreateReview_UserAlreadyHasReviewedTheMovie_ThrowsUserHasExistingReviewException()
    {
        // Arrange
        var movieId = 550;
        var existingUser = TestUtil.CreateGenericUser();
        var reviewId = Guid.NewGuid();
        Review existingReview = new()
        {
            UserId = existingUser.Id,
            CreationDate = DateTime.Now,
            LastUpdatedDate = DateTime.Now,
            ReviewText = "meh",
            MovieId = movieId,
            ReviewId = reviewId,
            Rating = 10,
            Votes = new List<Vote>()
        };
        
        List<Review> usersReviews = new() { existingReview};

        var handler = CreateHandler(
            repository => repository.Setup(x => x.GetUsersReviews(existingUser.Id)).ReturnsAsync(usersReviews),
            auth => auth.Setup(x => x.GetUserById(existingUser.Id)).ReturnsAsync(existingUser)
        );

        CreateReviewForMovieCommand testCommand = new(movieId, existingUser.Id, 5, "test");
        
        // Act & Assert
        Assert.ThrowsAsync<UserHasExistingReviewException>(() => handler.Handle(testCommand, new CancellationToken()));
    }


    private CreateReviewForMovieHandler CreateHandler(
        Action<Mock<ICreateReviewForMovieRepository>>? configureRepository = null,
        Action<Mock<IAuthenticationRepository>>? configureAuth = null)
    {
        configureRepository?.Invoke(_repositoryMock);
        configureAuth?.Invoke(_authMock);

        return new CreateReviewForMovieHandler(_repositoryMock.Object, _authMock.Object, _loggerMock.Object);
    }
}