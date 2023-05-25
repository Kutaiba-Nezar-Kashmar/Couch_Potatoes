using MediatR;
using Microsoft.Extensions.Logging;
using User.Domain;
using User.Infrastructure;

namespace User.Application;

public record GetUsersQuery(IReadOnlyCollection<string> userIds) : IRequest<IReadOnlyCollection<CouchPotatoUser>>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IReadOnlyCollection<CouchPotatoUser>>
{
    private readonly ILogger _logger;
    private readonly IAuthenticationRepository _auth;

    public GetUsersHandler(ILogger<GetUsersHandler> logger, IAuthenticationRepository auth)
    {
        _logger = logger;
        _auth = auth;
    }

    public async Task<IReadOnlyCollection<CouchPotatoUser>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var getUserTasks = request.userIds.Select(_auth.GetUserById);
            var users = await Task.WhenAll(getUserTasks);
            return users
                .Where(user => user != null)
                .Select(user => user!)
                .ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(1,e, $"Failed to process {nameof(Handle)} in {nameof(GetUsersHandler)}: {e}");
            throw;
        }
    }
}