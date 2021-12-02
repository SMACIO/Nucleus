using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.SimpleStateChecking;
using Nucleus.Users;

namespace Nucleus.Authorization.Permissions
{
    public class RequireAuthenticatedSimpleStateChecker<TState> : ISimpleStateChecker<TState>
        where TState : IHasSimpleStateCheckers<TState>
    {
        public Task<bool> IsEnabledAsync(SimpleStateCheckerContext<TState> context)
        {
            return Task.FromResult(context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated);
        }
    }
}


