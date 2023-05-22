using CQRS.Domain.Helpers.v1;
using FluentValidation;

namespace CQRS.Domain.Commands.v1.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory");

        RuleFor(x => x.Cpf)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory")
            .Must(StringHelper.IsCpf).WithMessage("The field {PropertyName} is not valid for {PropertyName}");

        RuleFor(x => x.DateBirth)
            .NotEmpty().WithMessage("The field {PropertyName} is mandatory");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("The field {PropertyName} is not valid").When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}