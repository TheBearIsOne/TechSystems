using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class UpdateSignalRequestValidator : AbstractValidator<UpdateSignalRequest>
{
    public UpdateSignalRequestValidator()
    {
        RuleFor(x => x.Status).MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.Status));
        RuleFor(x => x.SignalType).MaximumLength(100).When(x => x.SignalType is not null);
        RuleFor(x => x.EntityType).MaximumLength(50).When(x => x.EntityType is not null);
    }
}
