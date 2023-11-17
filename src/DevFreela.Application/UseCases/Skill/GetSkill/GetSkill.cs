

using DevFreela.Application.UseCases.Skill.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Skill.GetSkill;
public class GetSkill : IGetSkill
{
    private readonly ISkillRepository _skillRepository;

    public GetSkill(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<SkillModelOutput>> Handle
        (GetSkillInput request, CancellationToken cancellationToken)
    {
        var skill = _skillRepository.GetAll();

        if (skill == null)
        {
            return null;
        }

        var skillViewModel = skill
            .Select(s => new SkillModelOutput(s.Id, s.SkillName))
            .ToList();
        return skillViewModel;

    }
}
