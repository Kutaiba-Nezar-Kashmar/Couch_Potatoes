using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.GetReviewsForUser.Exceptions;
using User.Application.GetReviewsForUser.Repository;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.GetReviewsForUser;

public record GetReviewsForUserQuery(string userId) : IRequest<IReadOnlyCollection<Review>>;

public class GetReviewsForUserHandler : IRequestHandler<GetReviewsForUserQuery, IReadOnlyCollection<Review>>
{
    private readonly ILogger _logger;
    private readonly IAuthenticationRepository _auth;
    private readonly IGetReviewsForUserRepository _repository;

    public GetReviewsForUserHandler(ILogger<GetReviewsForUserHandler> logger, IGetReviewsForUserRepository repository,
        IAuthenticationRepository auth)
    {
        _logger = logger;
        _repository = repository;
        _auth = auth;
    }

    public async Task<IReadOnlyCollection<Review>> Handle(GetReviewsForUserQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _auth.GetUserById(request.userId);
            if (user is null)
            {
                throw new UserDoesNotExistException(request.userId);
            }

            return await _repository.GetReviewsForUser(request.userId);
        }
        catch (Exception e) when (e is not UserDoesNotExistException)
        {
            _logger.LogError(LogEvent.Application, e,
                $"Failed to process {nameof(Handle)} in {nameof(GetReviewsForUserHandler)}: {e}");
            throw new FailedToRetrieveReviewsForUserException(request.userId);
        }
    }
}