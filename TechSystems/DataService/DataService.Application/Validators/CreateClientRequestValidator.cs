using DataService.Application.Requests;
using FluentValidation;

namespace DataService.Application.Validators;

public sealed class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.MiddleName).MaximumLength(100).When(x => x.MiddleName is not null);
        RuleFor(x => x.BirthDate).LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
        RuleFor(x => x.PassportSeries).MaximumLength(4).When(x => x.PassportSeries is not null);
        RuleFor(x => x.PassportNumber).MaximumLength(6).When(x => x.PassportNumber is not null);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Email).MaximumLength(255).EmailAddress().When(x => x.Email is not null);
    }
}
