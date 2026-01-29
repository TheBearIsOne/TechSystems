using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class CreateLoanApplicationRequestValidator : AbstractValidator<CreateLoanApplicationRequest>
{
    public CreateLoanApplicationRequestValidator()
    {
        RuleFor(x => x.ClientId).GreaterThan(0);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.RequestedAmount).GreaterThan(0);
        RuleFor(x => x.RequestedTerm).GreaterThan(0);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
    }
}
