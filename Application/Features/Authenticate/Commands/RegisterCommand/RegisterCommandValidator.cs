using FluentValidation;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("La propiedad {PropertyName} debe ser un email válido.")
                .MaximumLength(30).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .MaximumLength(15).WithMessage("La propiedad {PropertyName} no puede exceder {MaxLength} caracteres.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")
                    .WithMessage("La propiedad {PropertyName} debe contener entre 8 a 15 caracteres, al menos una letra mayúscula, una letra minúscula, un número y un caracter especial.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("La propiedad {PropertyName} no puede estar vacia.")
                .Equal(p => p.Password).WithMessage("La propiedad {PropertyName} debe ser igual a Password.");
        }
    }
}
