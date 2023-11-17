using DevFreela.Application.UseCases.Project.CreateProject;
using FluentValidation;


namespace DevFreela.Application.Validators;
public class CreteProjectInputValidator : AbstractValidator<CreateProjectInput>
{
    public CreteProjectInputValidator()
    {
        RuleFor(p => p.Title)
            .MaximumLength(30)
            .WithMessage("Tamanho máximo de título é de 30 caracteres");

        RuleFor(p => p.Description)
            .MaximumLength(255)
            .WithMessage("Tamanho máximo de descrição é de 255 caracteres");

        RuleFor(p => p.TotalCost)
            .GreaterThan(0)
            .WithMessage("O custo total deve ser maior que zero");
    }
}
