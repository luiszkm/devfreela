using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.ListProject;
internal class ListProject : IListProject
{
    private readonly IProjectRepository _projectRepository;

    public ListProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ListProjectOutput> Handle(ListInputProject request, CancellationToken cancellationToken)
    {
        var searchOutput = await _projectRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir), cancellationToken);

        var output = new ListProjectOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(ProjectModelOutput.FromProject)
                .ToList());

        return output;
    }
}
