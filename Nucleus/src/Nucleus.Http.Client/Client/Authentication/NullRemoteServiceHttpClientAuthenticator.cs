using System.Threading.Tasks;
using Nucleus.DependencyInjection;

namespace Nucleus.Http.Client.Authentication
{
    [Dependency(TryRegister = true)]
    public class NullRemoteServiceHttpClientAuthenticator : IRemoteServiceHttpClientAuthenticator, ISingletonDependency
    {
        public Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
        {
            return Task.CompletedTask;
        }
    }
}

