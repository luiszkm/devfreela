using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities;
public class Skill : AggregateRoot
{
    public Skill(string skillName)
    {
        SkillName = skillName;
    }

    public string SkillName { get; private set; }

}