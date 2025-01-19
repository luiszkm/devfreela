using MediatR;

namespace DevFreela.Application.UseCases.Project.FinishedProject;
public class FinishedProjectInput : IRequest<bool>
{


    public FinishedProjectInput(Guid id, Guid idUser, Guid idProject, decimal totalCost)
    {
        Id = id;
        IdUser = idUser;
        IdProject = idProject;
        TotalCost = totalCost;
    }

    public Guid Id { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdProject { get; set; }

    public decimal TotalCost { get; set; }

}
