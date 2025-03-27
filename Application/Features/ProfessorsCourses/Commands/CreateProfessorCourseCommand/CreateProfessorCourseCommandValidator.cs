using FluentValidation;

namespace Application.Features.ProfessorsCourses.Commands.CreateProfessorCourseCommand
{
    public class CreateProfessorCourseCommandValidator : AbstractValidator<CreateProfessorCourseCommand>
    {
        public CreateProfessorCourseCommandValidator()
        {
            RuleFor(s => s.ProfessorId)
                .NotNull().WithMessage("La propiedad {PropertyName} no puede estar vacia.");

            RuleFor(s => s.CourseId)
                .NotNull().WithMessage("La propiedad {PropertyName} no puede estar vacia.");
        }
    }
}
