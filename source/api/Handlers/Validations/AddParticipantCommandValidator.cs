using connect_s_events_api.Handlers;
using FluentValidation;

namespace connect_s_event_api.Handlers.Validations;

public class AddParticipantCommandValidator : AbstractValidator<AddParticipantCommand>
{
    public AddParticipantCommandValidator()
    {
        RuleFor(_ => _.EventActivityId).NotEmpty();
        RuleFor(_ => _.Email)
            .NotEmpty()
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        RuleFor(_ => _.Name).NotEmpty();
        RuleFor(_ => _.UserId).NotEmpty();

    }
}
