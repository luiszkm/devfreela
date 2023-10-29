using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.CreateProject;

public interface ICreateProject : IRequestHandler<CreateProjectInput, ProjectModelOutput> { }


