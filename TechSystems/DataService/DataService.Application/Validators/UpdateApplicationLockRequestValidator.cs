using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class UpdateApplicationLockRequestValidator : AbstractValidator<UpdateApplicationLockRequest>
{
    public UpdateApplicationLockRequestValidator()
    {
        RuleFor(x => x.ExpiresAt).GreaterThan(DateTime.UtcNow).When(x => x.ExpiresAt.HasValue);
        RuleFor(x => x.LockedBy).GreaterThan(0);
    }
}
