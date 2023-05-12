using System.Text.Json;
using System.Text.Json.Serialization;
using EventService.Domain;
using EventService.Infrastructure;
using Google.Cloud.Firestore;

namespace EventService.Application.FetchEvents.Repository;

public class FetchEventSchema : IFetchEvents
{
    private CollectionReference _reference;

    public FetchEventSchema()
    {
        _reference = FirestoreDbReference.GetFirestoreDb().Collection("Events");
    }

    public async Task<IReadOnlyCollection<EventSchema>>
        FetchEventSchemaForServices(string serviceName)
    {
        List<EventSchema> eventsToReturn = null;
        var doc = _reference.Document(serviceName);
        var snapshot = await doc.GetSnapshotAsync();
        if (snapshot.Exists)
        {
            var elements = snapshot.ToDictionary();
            var events = elements["Events"];
            eventsToReturn = JsonSerializer.Deserialize<List<EventSchema>>(
                JsonSerializer.Serialize(events, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                }),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                });
        }

        return eventsToReturn;
    }
}