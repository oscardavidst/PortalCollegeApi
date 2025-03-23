using FluentValidation;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(80).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(80).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("La propiedad {PropertyName} debe ser un email válido.")
                .MaximumLength(80).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(50).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MinimumLength(5).WithMessage("La propiedad {PropertyName} no puede se menor a {MinLength} caracteres.")
                .MaximumLength(20).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(20).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.")
                .Equal(p => p.Password).WithMessage("La propiedad {PropertyName} debe ser igual a Password.");
        }
    }
}
