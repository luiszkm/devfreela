

using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Domain.Domain.Repository;
public interface ISkillRepository
{
    List<Skill> GetAll();
}
