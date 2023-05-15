namespace EventService.Application.RegisterEventSchemas.Validation;

public interface IValidator<in T> where T : class
{
    public void Validate(T element);
}