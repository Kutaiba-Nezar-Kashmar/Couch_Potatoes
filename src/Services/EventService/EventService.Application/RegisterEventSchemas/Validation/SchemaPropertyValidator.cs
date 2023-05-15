using EventService.Domain;

namespace EventService.Application.RegisterEventSchemas.Validation;

public class SchemaPropertyValidator : IValidator<SchemaProperty>
{
    private Guard _guard = new();

    public void Validate(SchemaProperty element)
    {
        _guard.AgainstNull(element)
            .AgainstNullOrEmptyString(element.Title, "Property must have a title");

        element.Properties.ToList().ForEach(Validate);
    }
}