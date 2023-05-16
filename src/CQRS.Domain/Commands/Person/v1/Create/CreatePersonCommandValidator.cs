using CQRS.Domain.Helpers.v1;
using FluentValidation;

namespace CQRS.Domain.Commands.Person.v1.Create;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory");

        RuleFor(x => x.Cpf)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory")
            .Must(value => value.IsCpf()).WithMessage("The field {PropertyName} is not valid for {PropertyName}");

        RuleFor(x => x.DateBirth)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("The field {PropertyName} is not valid")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}