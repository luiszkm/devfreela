

using DevFreela.Infrastructure.Persistence.Repository;

namespace DevFreela.IntegrationTest.Infra.Repositories.Skills;

[Collection(nameof(SkillsRepositoryTestFixture))]
public class SkillsRepositoryTest
{

    private readonly SkillsRepositoryTestFixture _fixture;

    public SkillsRepositoryTest(SkillsRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(InsertSkill))]
    [Trait("Infra", "SkillsRepository - Repository")]

    public async Task InsertSkill()
    {
     
    }
}
