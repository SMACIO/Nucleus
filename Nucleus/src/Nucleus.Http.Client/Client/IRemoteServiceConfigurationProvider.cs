using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Nucleus.Http.Client
{
    public interface IRemoteServiceConfigurationProvider
    {
        [ItemNotNull]
        Task<RemoteServiceConfiguration> GetConfigurationOrDefaultAsync(string name);
        
        [ItemCanBeNull]
        Task<RemoteServiceConfiguration> GetConfigurationOrDefaultOrNullAsync(string name);
    }
}
