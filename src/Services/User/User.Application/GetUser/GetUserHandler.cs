using MediatR;
using Microsoft.Extensions.Logging;
using User.Domain;
using User.Domain.Exceptions;
using User.Infrastructure;

namespace User.Application.GetUser;

public record GetUserQuery(string userId) : IRequest<CouchPotatoUser>;

public class GetUserHandler : IRequestHandler<GetUserQuery, CouchPotatoUser>
{
    private ILogger _logger;
    private IAuthenticationRepository _auth;

    public GetUserHandler(IAuthenticationRepository auth, ILogger<GetUserHandler> logger)
    {
        _auth = auth;
        _logger = logger;
    }

    public async Task<CouchPotatoUser> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _auth.GetUserById(request.userId);
        if (user is null)
        {
            throw new UserDoesNotExistException(request.userId);
        }

        return user;
    }
}