using FluentValidation;

namespace Application.Features.Students.Commands.CreateStudentCommand
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");

            RuleFor(s => s.LastName)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(50).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("La propiedad {PropertyName} debe ser un email válido.");
        }
    }
}
