using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class UpdatePaymentRequestValidator : AbstractValidator<UpdatePaymentRequest>
{
    public UpdatePaymentRequestValidator()
    {
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
    }
}
