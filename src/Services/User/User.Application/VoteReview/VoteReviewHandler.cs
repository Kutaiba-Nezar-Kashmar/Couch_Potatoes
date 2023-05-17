using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.GetReviewsForMovie;
using User.Application.UpvoteReview.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.UpvoteReview;

public record VoteReviewCommand(string userId, int movieId, Guid reviewId, VoteDirection direction) : IRequest;

public class VoteReviewHandler : IRequestHandler<VoteReviewCommand>
{
    private readonly ILogger _logger;
    private readonly IVoteReviewRepository _repository;
    private readonly IAuthenticationRepository _auth;
    private readonly IMediator _mediator;

    public VoteReviewHandler(ILogger<VoteReviewHandler> logger, IVoteReviewRepository repository,
        IAuthenticationRepository auth, IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _auth = auth;
        _mediator = mediator;
        _mediator = mediator;
    }

    public async Task Handle(VoteReviewCommand request, CancellationToken cancellationToken)
    {
        var user = await _auth.GetUserById(request.userId);

        if (user is null)
        {
            throw new UserDoesNotExistException(request.userId);
        }

        var reviews = await _mediator.Send(new GetReviewsForMovieQuery(request.movieId));

        var reviewDoesNotExist = reviews.All(r => r.ReviewId != request.reviewId);
        if (reviewDoesNotExist)
        {
            throw new ReviewDoesNotExistException(request.reviewId, request.movieId);
        }

        var review = reviews.First(r => r.ReviewId == request.reviewId);
        var existingVote = review.Votes.FirstOrDefault(v => v.UserId == request.userId);

        if (existingVote is null)
        {
            var newVote = new Vote
            {
                Direction = request.direction,
                UserId = request.userId,
                Id = Guid.NewGuid()
            };
            await _repository.VoteReview(request.movieId, review, newVote);
            return;
        }

        existingVote.Direction = request.direction;
        await _repository.VoteReview(request.movieId, review, existingVote);
    }
}