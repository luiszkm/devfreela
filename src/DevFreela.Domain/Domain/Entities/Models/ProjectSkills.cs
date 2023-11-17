
using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities.Models;
public class ProjectSkills : AggregateRoot
{

    public ProjectSkills(Guid idProject, Guid idSkill)
    {
        IdProject = idProject;
        IdSkill = idSkill;
    }

    public Guid IdProject { get; private set; }
    public Guid IdSkill { get; private set; }

    // EF Relation
    public Project? Project { get; private set; }
    public Skill? Skill { get; private set; }
}
