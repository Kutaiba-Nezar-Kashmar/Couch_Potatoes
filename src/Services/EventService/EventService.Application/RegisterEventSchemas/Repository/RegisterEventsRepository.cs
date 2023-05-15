using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using EventService.Application.RegisterEventSchemas.Exceptions;
using EventService.Domain;
using Google.Apis.Util;
using Google.Cloud.Firestore;

namespace EventService.Application.RegisterEventSchemas.Repository;

public class RegisterEventsRepository : IRegisterEventsRepository
{
    private readonly CollectionReference _reference;

    public RegisterEventsRepository()
    {
        _reference = Infrastructure.FirestoreDb.GetFirestoreDb().Collection("Events");
    }

    public async Task RegisterSchemasForService(string serviceName, ICollection<EventSchema> schemas)
    {
        var documentRef = _reference.Document(serviceName);

        schemas
            .Select(EventSchemaFirebaseDto.FromDomainSchema)
            .ToList()
            .ForEach(async schema => await documentRef.UpdateAsync("Events", schema));
    }

    public async Task<bool> DocumentForServiceExists(string serviceName)
    {
        return (await _reference.Document(serviceName).GetSnapshotAsync()).Exists;
    }

    public async Task CreateDocumentForService(string serviceName)
    {
        Dictionary<string, object> newDocument = new()
        {
            {"Events", new List<object>()}
        };

        var documentReference = _reference.Document(serviceName);
        var res = await documentReference.SetAsync(newDocument);

        if (res is null)
        {
            throw new FailedToCreateDocumentException($"Failed to create document for service: {serviceName}");
        }
    }
}