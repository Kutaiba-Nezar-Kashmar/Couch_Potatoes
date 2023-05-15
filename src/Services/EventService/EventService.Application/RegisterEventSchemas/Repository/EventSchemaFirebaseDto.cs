using EventService.Domain;
using Google.Cloud.Firestore;

namespace EventService.Application.RegisterEventSchemas.Repository;

[FirestoreData]
public class EventSchemaFirebaseDto
{
    [FirestoreProperty]
    public string Service { get; set; }

    [FirestoreProperty]
    public ContentSchemaDto Content { get; set; }

    [FirestoreProperty]
    public string Routingkey { get; set; }

    public static EventSchemaFirebaseDto FromDomainSchema(EventSchema domainSchema)
    {
        var dto = new EventSchemaFirebaseDto()
        {
            Service = domainSchema.Service,
            Routingkey = domainSchema.Routingkey,
            Content = new ContentSchemaDto()
            {
                Properties = domainSchema.Content.Properties
                    .Select(MapProperty)
                    .ToList()
            }
        };

        return dto;
    }

    private static SchemaPropertyDto MapProperty(SchemaProperty property)
    {
        // NOTE: (mibui 2023-05-15) This needs to be recursive since we don't know how deep the objects are.
        var dto = new SchemaPropertyDto()
        {
            Title = property.Title,
            Type = property.Type.ToString(),
            Properties = property.Properties.Select(MapProperty).ToList()
        };

        return dto;
    }
}

[FirestoreData]
public class ContentSchemaDto
{
    [FirestoreProperty]
    public IReadOnlyCollection<SchemaPropertyDto> Properties { get; set; }
}

[FirestoreData]
public class SchemaPropertyDto
{
    [FirestoreProperty]
    public string Title { get; set; }

    [FirestoreProperty]
    public string Type { get; set; }

    [FirestoreProperty]
    public IReadOnlyCollection<SchemaPropertyDto> Properties { get; set; }
}