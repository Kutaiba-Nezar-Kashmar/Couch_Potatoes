using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.UpdateProfileInfo.Exceptions;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.UpdateProfileInfo;

public record UpdateProfileInfoCommand
    (string userId, string newDisplayName, string newAvatarUri) : IRequest<CouchPotatoUser>;

public class UpdateProfileInfoHandler : IRequestHandler<UpdateProfileInfoCommand, CouchPotatoUser>
{
    private readonly IAuthenticationRepository _auth;
    private readonly ILogger _logger;

    public UpdateProfileInfoHandler(IAuthenticationRepository auth, ILogger<UpdateProfileInfoHandler> logger)
    {
        _logger = logger;
        _auth = auth;
    }

    public async Task<CouchPotatoUser> Handle(UpdateProfileInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingUser = await _auth.GetUserById(request.userId);
            if (existingUser is null)
            {
                _logger.LogError(LogEvent.Application, $"Failed to find user with id {request.userId}");
                throw new UserDoesNotExistException(request.userId);
            }

            var updatedUser = await _auth.UpdateUserProfile(existingUser.Id, request.newDisplayName, request.newAvatarUri);
            if (updatedUser is null)
            {
                _logger.LogError(LogEvent.Application, $"Failed to update user with id {request.userId}");
                throw new FailedToUpdateUserProfileInfoException(request.userId);
            }

            return updatedUser;
        }
        catch (Exception e) when (e is not UserDoesNotExistException and not FailedToUpdateUserProfileInfoException)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}