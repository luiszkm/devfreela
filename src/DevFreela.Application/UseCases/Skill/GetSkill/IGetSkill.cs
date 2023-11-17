
using DevFreela.Application.UseCases.Skill.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Skill.GetSkill;
public interface IGetSkill : IRequestHandler<
GetSkillInput, List<SkillModelOutput>>
{
}
