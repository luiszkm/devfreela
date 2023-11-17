

namespace DevFreela.Application.UseCases.Skill.Common;
public class SkillModelOutput
{
    public SkillModelOutput(
        Guid id,
        string description)
    {
        Id = id;
        Description = description;
    }

    public Guid Id { get; private set; }
    public string Description { get; private set; }
}
