using FluentValidation;

namespace Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.");

            RuleFor(s => s.Name)
                //.NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} ({PropertyValue}) no puede exceder {MaxLength} caracteres.");

            RuleFor(s => s.Credits)
                //.NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .InclusiveBetween(0, 255).WithMessage("La propiedad {PropertyName} debe estar entre 0 y 255");
        }
    }
}
