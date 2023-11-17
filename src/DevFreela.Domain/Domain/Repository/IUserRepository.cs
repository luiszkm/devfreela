
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByEmailAndPassword(string email, string passwordHash);
    Task CreateSession(string email, string password);

}
