using EventService.Domain;

namespace EventService.Application.RegisterEventSchemas.Validation;

public class EventSchemaValidator : IValidator<EventSchema>
{
    private readonly Guard _guard = new();

    public void Validate(EventSchema element)
    {
        _guard
            .AgainstNull(element, "Schema is null")
            .AgainstNullOrEmptyString(element.Routingkey, "Routing key cannot be null");

        new ContentSchemaValidator().Validate(element.Content);
    }
}