
using System.Text.RegularExpressions;
using DevFreela.Application.UseCases.User.CreateUser;
using FluentValidation;

namespace DevFreela.Application.Validators;
public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
{
    public CreateUserInputValidator()
    {
        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("Email inválido");

        RuleFor(u => u.Password)
            .Must(ValidatePassword)
            .WithMessage("Senha deve seguir os padroes de segurança");

        RuleFor(u => u.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nome é obrigatório");
    }

    public bool ValidatePassword(string password)
    {
        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
        return regex.IsMatch(password);
    }

}
