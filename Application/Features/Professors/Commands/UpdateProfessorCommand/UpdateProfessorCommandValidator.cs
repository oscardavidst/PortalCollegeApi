using FluentValidation;

namespace Application.Features.Professors.Commands.UpdateProfessorCommand
{
    public class UpdateProfessorCommandValidator : AbstractValidator<UpdateProfessorCommand>
    {
        public UpdateProfessorCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.");

            RuleFor(s => s.Name)
                //.NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");

            RuleFor(s => s.LastName)
                //.NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");
        }
    }
}
