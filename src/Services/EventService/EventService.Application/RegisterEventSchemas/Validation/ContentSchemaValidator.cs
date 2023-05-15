using EventService.Domain;

namespace EventService.Application.RegisterEventSchemas.Validation;

public class ContentSchemaValidator : IValidator<ContentSchema>
{
    public void Validate(ContentSchema element)
    {
        var propertyValidator = new SchemaPropertyValidator();
        element.Properties.ToList().ForEach(propertyValidator.Validate);
    }
}