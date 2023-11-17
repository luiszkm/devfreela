

using DevFreela.Application.UseCases.Skill.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Skill.GetSkill;

public class GetSkillInput : IRequest<SkillModelOutput>, IRequest<List<SkillModelOutput>>
{
}
