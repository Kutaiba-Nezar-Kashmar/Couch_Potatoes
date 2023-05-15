using EventService.Domain;

namespace EventService.Application.RegisterEventSchemas.Repository;

public interface IRegisterEventsRepository
{
    public Task RegisterSchemasForService(string serviceName, ICollection<EventSchema> schemas);
    public Task<bool> DocumentForServiceExists(string serviceName);
    public Task CreateDocumentForService(string serviceName);
}
