using System.Threading.Tasks;

namespace Nucleus.Authorization
{
    public interface IMethodInvocationAuthorizationService
    {
        Task CheckAsync(MethodInvocationAuthorizationContext context);
    }
}
