using EventService.Domain;

namespace EventService.Application.FetchEvents.Repository;

public interface IFetchEvents
{
    Task<IReadOnlyCollection<EventSchema>> FetchEventSchemasForService(string serviceName);
}