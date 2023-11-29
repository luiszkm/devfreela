
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmail(string email);

    Task<User?> AddSkill(Guid userId, List<Skill> skill);

    Task<List<UserSkills>>? GetUserWithSkills(Guid id);

}
