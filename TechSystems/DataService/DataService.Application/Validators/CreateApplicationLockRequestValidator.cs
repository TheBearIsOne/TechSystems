using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class CreateApplicationLockRequestValidator : AbstractValidator<CreateApplicationLockRequest>
{
    public CreateApplicationLockRequestValidator()
    {
        RuleFor(x => x.LockId).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ApplicationId).GreaterThan(0);
        RuleFor(x => x.LockedBy).GreaterThan(0);
    }
}
