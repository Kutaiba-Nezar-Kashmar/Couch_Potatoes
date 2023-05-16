using MediatR;
using Metrics.Domain.Models.Person;
using Microsoft.Extensions.Logging;

namespace Metrics.Application.PersonMetrics;

public record PersonMetricHandlerRequest() : IRequest<PersonStatistics>;

public class PersonMetricsHandler : IRequestHandler<PersonMetricHandlerRequest,
    PersonStatistics>
{
    private readonly ILogger<PersonMetricsHandler> _logger;
    
    public Task<PersonStatistics> Handle
    (
        PersonMetricHandlerRequest request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}