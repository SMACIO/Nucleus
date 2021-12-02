using System.Threading.Tasks;

namespace Nucleus.MultiTenancy
{
    public interface ITenantConfigurationProvider
    {
        Task<TenantConfiguration> GetAsync(bool saveResolveResult = false);
    }
}

