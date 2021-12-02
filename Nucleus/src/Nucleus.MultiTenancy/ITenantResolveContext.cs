using JetBrains.Annotations;
using Nucleus.DependencyInjection;

namespace Nucleus.MultiTenancy
{
    public interface ITenantResolveContext : IServiceProviderAccessor
    {
        [CanBeNull]
        string TenantIdOrName { get; set; }

        bool Handled { get; set; }
    }
}

