using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nucleus.DependencyInjection;

namespace Nucleus.Http.Client
{
    public class RemoteServiceConfigurationProvider : IRemoteServiceConfigurationProvider, IScopedDependency
    {
        protected NucleusRemoteServiceOptions Options { get; }

        public RemoteServiceConfigurationProvider(IOptionsMonitor<NucleusRemoteServiceOptions> options)
        {
            Options = options.CurrentValue;
        }

        public Task<RemoteServiceConfiguration> GetConfigurationOrDefaultAsync(string name)
        {
            return Task.FromResult(Options.RemoteServices.GetConfigurationOrDefault(name));
        }

        public Task<RemoteServiceConfiguration> GetConfigurationOrDefaultOrNullAsync(string name)
        {
            return Task.FromResult(Options.RemoteServices.GetConfigurationOrDefaultOrNull(name));
        }
    }
}




