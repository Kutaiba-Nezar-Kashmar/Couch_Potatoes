using Microsoft.Extensions.Logging;
using Moq;
using User.Application.UpvoteReview;
using User.Application.UpvoteReview.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Test;

[TestFixture]
public class VoteReviewTest
{
    private readonly Mock<IAuthenticationRepository> _authMock = new();
    private readonly Mock<IVoteReviewRepository> _repositoryMock = new();
    private readonly Mock<ILogger<VoteReviewHandler>> _loggerMock = new();

    [Test]
    public void VoteReview_UserDoesNotExist_ThrowsUserDoesExistException()
    {
        // Arrange
        var testUserId = "yW3zBelUzeWWO380vu8IiTsgxWq2";
        var handler = CreateHandler(null, (auth) =>
        {
            CouchPotatoUser nonExistingUser = null;
            auth.Setup(x => x.GetUserById(testUserId).Result).Returns(nonExistingUser);
        });
        VoteReviewCommand voteReviewCommand = new(testUserId, 1, Guid.NewGuid(), VoteDirection.Up);

        // Act & Assert
        Assert.ThrowsAsync<UserDoesNotExistException>(async () =>
            await handler.Handle(voteReviewCommand, new CancellationToken()));
    }

    [Test]
    public void VoteReview_ReviewDoesNotExist_ThrowsReviewDoesNotExistException()
    {
        // Arrange
        var testUserId = "yW3zBelUzeWWO380vu8IiTsgxWq2";
        var movieId = 1;
        var handler = CreateHandler(
            (repository) =>
            {
                repository.Setup(x => x.GetReviewsForMovie(movieId).Result).Returns(new List<Review>());
            },
            (auth) =>
            {
                CouchPotatoUser user = new()
                {
                    Id = testUserId,
                    FavoriteMovies = new List<int>(),
                    Email = "test@hest.com",
                    DisplayName = "TEST"
                };
                auth.Setup(x => x.GetUserById(testUserId).Result).Returns(user);
            });
        VoteReviewCommand voteReviewCommand = new(testUserId, 1, Guid.NewGuid(), VoteDirection.Up);

        // Act & Assert
        Assert.ThrowsAsync<ReviewDoesNotExistException>(async () =>
            await handler.Handle(voteReviewCommand, new CancellationToken()));
    }


    [Test]
    public async Task VoteReview_UserHasVoteInSameDirection_DeletesExistingVote()
    {
        // Arrange
        var testUserId = "yW3zBelUzeWWO380vu8IiTsgxWq2";
        var testUserId2 = "yW3zBelUzeWWO380vu8IiTsgxWq1";
        var movieId = 1;
        var reviewId = Guid.NewGuid();

        var voteId = Guid.NewGuid();
        Vote existingVote = new()
        {
            Id = voteId,
            UserId = testUserId,
            Direction = VoteDirection.Up
        };
        Review review = new()
        {
            UserId = testUserId2,
            CreationDate = DateTime.Now,
            LastUpdatedDate = DateTime.Now,
            ReviewText = "It was decent I guess...",
            MovieId = 1,
            ReviewId = reviewId,
            Rating = 10,
            Votes = new List<Vote>()
            {
                existingVote
            }
        };

        var reviews = new List<Review>()
        {
            review
        };

        var handler = CreateHandler(
            (repository) => { repository.Setup(x => x.GetReviewsForMovie(movieId).Result).Returns(reviews); },
            (auth) =>
            {
                CouchPotatoUser user = new()
                {
                    Id = testUserId,
                    FavoriteMovies = new List<int>(),
                    Email = "test@hest.com",
                    DisplayName = "TEST"
                };
                auth.Setup(x => x.GetUserById(testUserId).Result).Returns(user);
            });
        VoteReviewCommand voteReviewCommand = new(testUserId, 1, reviewId, VoteDirection.Up);

        // Act 
        await handler.Handle(voteReviewCommand, new CancellationToken());

        // Assert
        _repositoryMock.Verify(mock => mock.DeleteVote(movieId, reviewId, voteId), Times.Once);
    }


    private VoteReviewHandler CreateHandler(Action<Mock<IVoteReviewRepository>>? configureRepository = null,
        Action<Mock<IAuthenticationRepository>>? configureAuth = null)
    {
        configureRepository?.Invoke(_repositoryMock);
        configureAuth?.Invoke(_authMock);

        return new VoteReviewHandler(_loggerMock.Object, _repositoryMock.Object, _authMock.Object);
    }
}