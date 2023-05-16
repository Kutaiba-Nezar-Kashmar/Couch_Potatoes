using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.API.Controllers.V1;

[ApiController]
[Route("api/v1/person_metrics")]
public class PersonMetricsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonMetricsController> _logger;

    public PersonMetricsController
    (
        IMediator mediator,
        ILogger<PersonMetricsController> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }
}