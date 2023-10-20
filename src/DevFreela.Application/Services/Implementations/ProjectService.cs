using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.InputModels;
using DevFreela.Domain.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Implementations;
public class ProjectService : IProjectServices
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ProjectViewModel> GetAll(string query)
    {
        var projects = _dbContext.Projects
            .Select(p => new ProjectViewModel(p.Title, p.CreatedAt))
            .ToList();

        return projects;

    }

    public ProjectDetailsViewModel GetById(Guid id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        var output = new ProjectDetailsViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt
            );
        return output;
    }

    public Guid Create(NewProjectInputModel inputModel)
    {
        var project = new Project(
            inputModel.Title,
            inputModel.Description,
            inputModel.IdClient,
            inputModel.IdFreelancer,
            inputModel.TotalCost);

        _dbContext.Projects.Add(project);

        return project.Id;
    }

    public void Update(UpdateProjectInputModel inputModel)
    {
        var project = VerifyIfProjectExists(inputModel.Id);
        project.Update(
            inputModel.Title,
            inputModel.Description,
            inputModel.TotalCost);
    }

    public void Delete(Guid id)
    {
        var project = VerifyIfProjectExists(id);

        _dbContext.Projects.Remove(project);
        project.Cancel();
    }

    public void Start(Guid id)
    {
        var project = VerifyIfProjectExists(id);
        project.Start();

    }

    public void Finish(Guid id)
    {
        var project = VerifyIfProjectExists(id);
        project.Finish();
    }

    public void AddComment(AddCommentInputModel inputModel)
    {
        var projectComment = new ProjectComment(
            inputModel.Content,
            inputModel.IdProject,
            inputModel.IdUser);

        _dbContext.ProjectComments.Add(projectComment);
    }

    private Project VerifyIfProjectExists(Guid id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

        if (project == null)
        {
            throw new Exception("Projeto não encontrado");
        }

        return project;
    }
}
