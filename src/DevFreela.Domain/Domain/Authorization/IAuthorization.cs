
using DevFreela.Domain.Domain.Enums;

namespace DevFreela.Domain.Domain.Authorization;
public interface IAuthorization
{
    string GenerateToken(Guid userId, UserRole role);
    string ComputeSha256Hash(string password);

}
