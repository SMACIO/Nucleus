using System.Threading.Tasks;

namespace Nucleus.Authorization
{
    public class AlwaysAllowMethodInvocationAuthorizationService : IMethodInvocationAuthorizationService
    {
        public Task CheckAsync(MethodInvocationAuthorizationContext context)
        {
            return Task.CompletedTask;
        }
    }
}
