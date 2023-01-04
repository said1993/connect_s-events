using connect_s_event_api.Exceptions;
using connect_s_events_domain.EventActivities;
using connect_s_events_domain.EventActivities.Models;
using FluentValidation;
using Mediator;

namespace connect_s_events_api.Handlers;

public class AddParticipantHandler : IRequestHandler<AddParticipantCommand, Unit>
{
    private readonly IValidator<AddParticipantCommand> _validator;
    private readonly IEventActivityRepository _repository;
    private readonly ILogger<AddParticipantHandler> _logger;

    public AddParticipantHandler(IEventActivityRepository repository, IValidator<AddParticipantCommand> validator, ILogger<AddParticipantHandler> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    public async ValueTask<Unit> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var validate = _validator.Validate(request);
        if (!validate.IsValid)
        {
            throw new InvalidRequestExcepetion(validate.Errors);
        }
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
