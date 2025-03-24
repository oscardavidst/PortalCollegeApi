using FluentValidation;

namespace Application.Features.Professors.Commands.CreateProfessorCommand
{
    public class CreateProfessorCommandValidator : AbstractValidator<CreateProfessorCommand>
    {
        public CreateProfessorCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");

            RuleFor(s => s.LastName)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");
        }
    }
}
