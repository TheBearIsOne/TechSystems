using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.LoanId).GreaterThan(0);
        RuleFor(x => x.PrincipalAmount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.InterestAmount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
    }
}
