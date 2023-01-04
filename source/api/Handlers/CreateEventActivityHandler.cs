using Mediator;
using connect_s_events_domain.EventActivities;
using connect_s_events_domain.EventActivities.Models;

namespace connect_s_events_api.Handlers;

public class CreateEventActivityHandler : IRequestHandler<CreateEventActivityRequest, Unit>
{
    private readonly IEventActivityRepository _repository;

    public CreateEventActivityHandler(IEventActivityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async ValueTask<Unit> Handle(
        CreateEventActivityRequest request,
        CancellationToken cancellationToken)
    {
        CheckRequest(request);
        return await AddEventActivity(request);
    }
    private void CheckRequest(CreateEventActivityRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
    }
    private async ValueTask<Unit> AddEventActivity(CreateEventActivityRequest request)
    {
        var eventActivity = request.ToEventActivity();
        await _repository.Add(eventActivity);
        return Unit.Value;
    }
}

public class CreateEventActivityRequest : IRequest<Unit>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid Owner { get; init; }
    public string OwnerEmail { get; init; }
    public DateTimeOffset BeginAt { get; init; }
    public DateTimeOffset EndAt { get; init; }

    public EventActivity ToEventActivity()
    {
        return EventActivity.Create(Name, Description, Owner, OwnerEmail, BeginAt, EndAt);
    }
}
