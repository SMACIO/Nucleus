using System;

namespace Nucleus.DependencyInjection
{
    public interface IClientScopeServiceProviderAccessor
    {
        IServiceProvider ServiceProvider { get; }
    }
}

