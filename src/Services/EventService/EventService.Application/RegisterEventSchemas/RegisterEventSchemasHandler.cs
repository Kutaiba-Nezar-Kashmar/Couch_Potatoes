using EventService.Application.RegisterEventSchemas.Exceptions;
using EventService.Application.RegisterEventSchemas.Repository;
using EventService.Application.RegisterEventSchemas.Validation;
using EventService.Domain;
using MediatR;

namespace EventService.Application.RegisterEventSchemas;

public record RegisterEventSchemasCommand(string serviceName, ICollection<EventSchema> schemas) : IRequest;

public class RegisterEventSchemasHandler : IRequestHandler<RegisterEventSchemasCommand>
{
    private readonly IRegisterEventsRepository _repository;

    public RegisterEventSchemasHandler(IRegisterEventsRepository repository)
    {
        _repository = repository;
    }


    public async Task Handle(RegisterEventSchemasCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.DocumentForServiceExists(request.serviceName))
        {
            await _repository.CreateDocumentForService(request.serviceName);
        }

        try
        {
            EventSchemaValidator validator = new();
            request.schemas.ToList().ForEach(validator.Validate);
        }
        catch (Exception e) when (e is ArgumentException or ArgumentNullException)
        {
            Console.WriteLine(e);
            throw new InvalidEventSchemaException(
                $"One or more event schemas for service {request.serviceName} is invalid");
        }

        await _repository.RegisterSchemasForService(request.serviceName, request.schemas);
    }
}