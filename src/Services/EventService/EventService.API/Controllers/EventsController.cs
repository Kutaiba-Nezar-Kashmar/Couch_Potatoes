using System.Net;
using AutoMapper;
using EventService.API.Dtos;
using EventService.Application.FetchEvents;
using EventService.Application.RegisterEventSchemas;
using EventService.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers;

[ApiController]
[Route("api/v1/events")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EventsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<EventSchema>>> EventsByService([FromQuery] string name)
    {
        try
        {
            var events = await _mediator.Send(new FetchEventsQuery(name));
            var eventDtos = events.Select(e => _mapper.Map<EventSchemaDto>(e)).ToList();
            return Ok(eventDtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(HttpStatusCode.InternalServerError.Cast<int>(), "Failed to fetch event schemas");
        }
    }

    [HttpPost("{service}")]
    public async Task<ActionResult> RegisterEventSchemas([FromRoute] string service,
        [FromBody] RegisterEventSchemasDto dto)
    {
        var domainSchemas = dto.schemas
            .Select(dtoSchema =>
            {
                dtoSchema.Service = service;
                return _mapper.Map<EventSchema>(dtoSchema);
            })
            .ToList();

        await _mediator.Send(new RegisterEventSchemasCommand(service, domainSchemas));
        return Ok();
    }
}