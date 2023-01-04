using connect_s_events_api.Handlers;
using Mediator;

namespace connect_s_events_api.Controllers;
[Route("api/activities")]
[ApiController]
public class EventActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventActivitiesController> _logger;

    public EventActivitiesController(IMediator mediator, ILogger<EventActivitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        using (_logger.BeginScope("Getting All Event Activities"))
        {
            _logger.LogInformation("Get All EventActivities");
            var eventActivities = await _mediator.Send(new GetEventActivitiesRequest());
            return Ok(eventActivities);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventActivityRequest createEventActivityRequest)
    {
        await _mediator.Send(createEventActivityRequest);
        return Created("", null);
    }

    [HttpPost("participants")]
    public async Task<IActionResult> AddParticipant([FromBody] AddParticipantCommand addParticipantCommand)
    {
        await _mediator.Send(addParticipantCommand);
        return Created("", null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteEventActivityCommand(id));
        return Accepted();
    }
}
