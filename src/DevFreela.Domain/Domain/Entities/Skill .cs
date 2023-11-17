using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities;
public class Skill : AggregateRoot
{
    public Skill(string skillName)
    {
        SkillName = skillName;
        _userSkills = new List<Guid>();
    }

    public string SkillName { get; private set; }
    private List<Guid> _userSkills { get; set; }

    public IReadOnlyList<Guid> UserSkills =>
        (IReadOnlyList<Guid>)_userSkills.AsReadOnly();


}