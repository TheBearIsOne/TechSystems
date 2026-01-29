using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class UpdateLoanApplicationRequestValidator : AbstractValidator<UpdateLoanApplicationRequest>
{
    public UpdateLoanApplicationRequestValidator()
    {
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
    }
}
