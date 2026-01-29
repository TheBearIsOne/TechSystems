using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class CreateSignalRequestValidator : AbstractValidator<CreateSignalRequest>
{
    public CreateSignalRequestValidator()
    {
        RuleFor(x => x.SignalType).NotEmpty().MaximumLength(100);
        RuleFor(x => x.EntityType).NotEmpty().MaximumLength(50);
        RuleFor(x => x.EntityId).GreaterThan(0);
    }
}
