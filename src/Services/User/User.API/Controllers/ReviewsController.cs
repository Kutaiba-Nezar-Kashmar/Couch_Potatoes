using Microsoft.AspNetCore.Mvc;
using User.API.Dtos;

namespace User.API.Controllers;

[ApiController]
[Route("api/v1/reviews")]
public class ReviewsController : ControllerBase
{
    public ReviewsController()
    {
    }

    [HttpPost("{movieId}")]
    public Task<ActionResult> Create([FromRoute] int movieId, [FromBody] ReviewDto reviewDto)
    {
        throw new NotImplementedException();
    }
}