using FluentValidation;

namespace Application.Features.Enrollments.Commands.CreateEnrollmentCommand
{
    public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
    {
        public CreateEnrollmentCommandValidator()
        {
            RuleFor(s => s.CourseId)
                .NotNull().WithMessage("La propiedad {PropertyName} no puede estar vacia.");

            RuleFor(s => s.StudentId)
                .NotNull().WithMessage("La propiedad {PropertyName} no puede estar vacia.");
        }
    }
}
