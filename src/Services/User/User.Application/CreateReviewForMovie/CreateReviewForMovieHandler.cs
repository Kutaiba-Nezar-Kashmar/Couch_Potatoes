using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.CreateReviewForMovie.Exceptions;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.CreateReviewForMovie;

public record CreateReviewForMovieCommand(int movieId, string userId, int rating, string reviewText) : IRequest<Review>;

public class CreateReviewForMovieHandler : IRequestHandler<CreateReviewForMovieCommand, Review>
{
    private readonly ICreateReviewForMovieRepository _repository;
    private readonly IAuthenticationRepository _auth;
    private readonly ILogger _logger;

    public CreateReviewForMovieHandler(ICreateReviewForMovieRepository repository, IAuthenticationRepository auth,
        ILogger<CreateReviewForMovieHandler> logger)
    {
        _repository = repository;
        _auth = auth;
        _logger = logger;
    }

    public async Task<Review> Handle(CreateReviewForMovieCommand request, CancellationToken cancellationToken)
    {
        var reviewToCreate = new Review
        {
            Rating = request.rating,
            MovieId = request.movieId,
            CreationDate = DateTime.UtcNow,
            LastUpdatedDate = DateTime.UtcNow,
            ReviewText = request.reviewText,
            UserId = request.userId,
            Votes = new List<Vote>(),
            ReviewId = Guid.NewGuid()
        };

        var user = await _auth.GetUserById(request.userId);
        if (user is null)
        {
            throw new UserDoesNotExistException(request.userId);
        }

        if (!reviewToCreate.IsValid())
        {
            throw new InvalidReviewException();
        }

        try
        {
            var reviews = await _repository.GetReviewsForMovie(request.movieId);

            var userAlreadyHasReviewForMovie = reviews.Any(r => r.UserId == request.userId);
            if (userAlreadyHasReviewForMovie)
            {
                throw new UserHasExistingReviewException(request.userId, request.movieId);
            }

            await _repository.CreateForMovie(reviewToCreate.MovieId, reviewToCreate);
            return reviewToCreate;
        }
        catch (Exception e) when (e is not UserHasExistingReviewException)
        {
            _logger.LogError(LogEvent.Application, e,
                $"Failed to process {nameof(Handle)} in {nameof(CreateReviewForMovieHandler)}");
            throw new FailedToCreateReviewException(reviewToCreate.MovieId, "", e);
        }
    }
}