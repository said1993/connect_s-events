using Mediator;
using connect_s_events_domain.EventActivities;
using connect_s_events_domain.EventActivities.Models;

namespace connect_s_events_api.Handlers;

public class AddParticipantHandler : IRequestHandler<AddParticipantCommand, Unit>
{
    private readonly IEventActivityRepository _repository;

    public AddParticipantHandler(IEventActivityRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async ValueTask<Unit> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var participant = Participant.Create(request.UserId, request.EventActivityId, request.Email, request.Name);
        await _repository.AddParticipant(participant);
        return Unit.Value;
    }
}

public record AddParticipantCommand(
    Guid UserId,
    Guid EventActivityId,
    string Email,
    string Name
    ) : IRequest<Unit>;
