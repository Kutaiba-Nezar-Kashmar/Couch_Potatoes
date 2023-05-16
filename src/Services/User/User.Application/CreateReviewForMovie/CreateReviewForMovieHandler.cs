using MediatR;
using User.Application.CreateReviewForMovie.Repository;
using User.Domain;
using User.Domain.Exceptions;

namespace User.Application.CreateReviewForMovie;

public record CreateReviewForMovieCommand(int movieId, Guid userId, float rating, string reviewText) : IRequest;

public class CreateReviewForMovieHandler : IRequestHandler<CreateReviewForMovieCommand>
{
    private readonly ICreateReviewForMovieRepository _repository;

    public CreateReviewForMovieHandler(ICreateReviewForMovieRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateReviewForMovieCommand request, CancellationToken cancellationToken)
    {
        var reviewToCreate = new Review
        {
            Rating = request.rating,
            MovieId = request.movieId,
            CreationDate = DateTime.UtcNow,
            ReviewText = request.reviewText,
            UserId = request.userId,
            Votes = new List<Vote>()
        };

        // TODO: Add check for if user exists and if movie exists.
        if (!reviewToCreate.IsValid())
        {
            throw new InvalidReviewException();
        }

        try
        {
            await _repository.CreateForMovie(reviewToCreate.MovieId, reviewToCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new FailedToCreateReviewException(reviewToCreate.MovieId);
        }
    }
}