using EventService.Application.FetchEvents.Repository;
using EventService.Domain;
using MediatR;

namespace EventService.Application.FetchEvents;

public record FetchEventsQuery(string serviceName) : IRequest<IReadOnlyCollection<EventSchema>>;

public class FetchEventsHandler : IRequestHandler<FetchEventsQuery, IReadOnlyCollection<EventSchema>>
{
    private readonly IFetchEvents _fetchEvents;

    public FetchEventsHandler(IFetchEvents fetchEvents)
    {
        _fetchEvents = fetchEvents;
    }

    public async Task<IReadOnlyCollection<EventSchema>> Handle(FetchEventsQuery request, CancellationToken cancellationToken)
    {
        return await _fetchEvents.FetchEventSchemaForServices(request.serviceName);
    }
}