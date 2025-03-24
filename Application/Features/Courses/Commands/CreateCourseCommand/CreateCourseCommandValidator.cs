using FluentValidation;

namespace Application.Features.Courses.Commands.CreateCourseCommand
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");

            RuleFor(s => s.Credits)
                .NotNull().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .InclusiveBetween(0, 255).WithMessage("La propiedad {PropertyName} debe estar entre 0 y 255");
        }
    }
}
