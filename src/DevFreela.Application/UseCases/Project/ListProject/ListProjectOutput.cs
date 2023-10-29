using DevFreela.Application.UseCases.Project.Common;


namespace DevFreela.Application.UseCases.Project.ListProject;
public class ListProjectOutput : PaginatedListOutput<ProjectModelOutput>
{
    public ListProjectOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<ProjectModelOutput> items) :
        base(page, perPage, total, items)
    {
    }
}
