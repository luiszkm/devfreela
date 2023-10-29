
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.seddwork.SearchbleRepository.cs;
using MediatR;

namespace DevFreela.Application.UseCases.Project.ListProject;
public class ListInputProject : PaginatedListInput, IRequest<ListProjectOutput>
{
    public ListInputProject(
        int page,
        int perPage,
        string search,
        string sort,
        SearchOrder dir)
        : base(page, perPage, search, sort, dir)
    {
    }
    public ListInputProject() : base(1, 15, "", "", SearchOrder.Asc) { }

}
