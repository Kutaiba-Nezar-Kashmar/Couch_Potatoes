using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using EventService.Domain;
using EventService.Infrastructure;
using Google.Cloud.Firestore;
using FirestoreDb = EventService.Infrastructure.FirestoreDb;

namespace EventService.Application.FetchEvents.Repository;

public class FetchEventSchema : IFetchEvents
{
    private readonly CollectionReference _reference;

    public FetchEventSchema()
    {
        _reference = FirestoreDb.GetFirestoreDb().Collection("Events");
    }

    public async Task<IReadOnlyCollection<EventSchema>>
        FetchEventSchemasForService(string serviceName)
    {
        List<EventSchema> eventsToReturn = new();
        var doc = _reference.Document(serviceName);
        var snapshot = await doc.GetSnapshotAsync();

        if (!snapshot.Exists)
        {
            return eventsToReturn;
        }

        var elements = snapshot.ToDictionary();
        var events = elements["Events"];

        eventsToReturn = JsonSerializer.Deserialize<List<EventSchema>>(
            JsonSerializer.Serialize(events, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {new JsonStringEnumConverter()}
            }),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = {new JsonStringEnumConverter()}
            }) ?? new List<EventSchema>();

        return eventsToReturn;
    }
}