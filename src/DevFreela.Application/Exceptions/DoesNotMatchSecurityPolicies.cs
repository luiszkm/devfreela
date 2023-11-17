
using System.Threading.Tasks;

namespace DevFreela.Application.Exceptions;
public class DoesNotMatchSecurityPolicies : Exception
{
    public DoesNotMatchSecurityPolicies() :
        base("the password not match the security policies")
    {
    }
}
