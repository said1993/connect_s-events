using Mediator;
using connect_s_events_domain.EventActivities;

namespace connect_s_events_api.Handlers;

public class DeleteEventActivityHandler : IRequestHandler<DeleteEventActivityCommand, Unit>
{
    private readonly IEventActivityRepository _repository;
    public DeleteEventActivityHandler(IEventActivityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async ValueTask<Unit> Handle(DeleteEventActivityCommand request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.EventActivityId, cancellationToken);
        return Unit.Value;
    }
}

public record DeleteEventActivityCommand(Guid EventActivityId) : IRequest<Unit>;
