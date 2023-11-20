

using DevFreela.E2ETest.Api.Project.Common;
using DevFreela.Infrastructure.Persistence.Repository;

namespace DevFreela.E2ETest.Api.Project;

[Collection(nameof(ProjectAPITestFixture))]
public class ListUsersInterestedInTheProject
{
    private readonly ProjectAPITestFixture _fixture;

    public ListUsersInterestedInTheProject(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(AddUserOnTheLisInterested))]
    [Trait("E2E/API", "Project/Create - Endpoints")]

    public async Task AddUserOnTheLisInterested()
    {
        var user = _fixture.GetValidUser();
        var project = _fixture.GetValidProject();
        var dbContext = _fixture.CreateApiDbContextInMemory();
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

    }
}
