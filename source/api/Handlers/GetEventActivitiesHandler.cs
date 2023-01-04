using Mediator;
using connect_s_events_domain.EventActivities;

namespace connect_s_events_api.Handlers;

public class GetEventActivitiesHandler : IRequestHandler<GetEventActivitiesRequest, IEnumerable<GetEventActivitiesResponse>>
{
    private readonly IEventActivityRepository _repository;
    private readonly ILogger<GetEventActivitiesHandler> _logger;

    public GetEventActivitiesHandler(IEventActivityRepository repository, ILogger<GetEventActivitiesHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async ValueTask<IEnumerable<GetEventActivitiesResponse>> Handle(GetEventActivitiesRequest request, CancellationToken cancellationToken)
    {
        var activities = await _repository.GetAll();
        _logger.LogInformation($"Getting {activities.Count()} successfully.");
        return activities.Select(_ => new GetEventActivitiesResponse(
            _.Id, _.Name, _.Description, _.Owner, _.OwnerEmail, _.CreatedAt,
            _.BeginAt, _.EndAt));
    }
}

public class GetEventActivitiesRequest : IRequest<IEnumerable<GetEventActivitiesResponse>>
{

}

public record GetEventActivitiesResponse(Guid Id, string Name,
    string Description, Guid Owner, string OwnerEmail,
    DateTimeOffset CreatedAt, DateTimeOffset BeginAt,
    DateTimeOffset EndAt);
