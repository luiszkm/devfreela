using DevFreela.Domain.Domain.Entities;


namespace DevFreela.Infrastructure.Models;
internal class ProjectSkills
{
    public ProjectSkills(Guid idProject, Guid idSkill)
    {
        IdProject = idProject;
        IdSkill = idSkill;
    }


    public Guid IdProject { get; set; }
    public Guid IdSkill { get; set; }
    public Skill Skill { get; set; }
    public User User { get; set; }

}
