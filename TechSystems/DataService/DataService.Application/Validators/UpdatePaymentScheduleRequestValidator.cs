using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class UpdatePaymentScheduleRequestValidator : AbstractValidator<UpdatePaymentScheduleRequest>
{
    public UpdatePaymentScheduleRequestValidator()
    {
        RuleFor(x => x.IsPaid).NotNull();
    }
}
