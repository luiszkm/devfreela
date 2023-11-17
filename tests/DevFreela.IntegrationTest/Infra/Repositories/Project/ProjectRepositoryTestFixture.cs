

using DevFreela.IntegrationTest.Base;

namespace DevFreela.IntegrationTest.Infra.Repositories.Project;

[CollectionDefinition(nameof(ProjectRepositoryTestFixture))]

public class ProjectRepositoryTestFixtureCollection : ICollectionFixture<ProjectRepositoryTestFixture>
{
}
public class ProjectRepositoryTestFixture : BaseFixture
{

    public Guid GetRandomGuid()
        => Guid.NewGuid();

    public DomainEntity.Project GetValidProject()
        => new(
            GetValidName(),
            GetValidDescription(),
            1000,
            GetRandomGuid());


    public List<DomainEntity.Project> GetExampleProjectList(int length = 10)
        => Enumerable.Range(1, length)
            .Select(_ => GetValidProject())
            .ToList();
}
