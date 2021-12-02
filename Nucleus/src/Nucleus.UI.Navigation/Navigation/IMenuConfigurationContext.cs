using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Nucleus.DependencyInjection;

namespace Nucleus.UI.Navigation
{
    public interface IMenuConfigurationContext : IServiceProviderAccessor
    {
        ApplicationMenu Menu { get; }

        IAuthorizationService AuthorizationService { get; }

        IStringLocalizerFactory StringLocalizerFactory { get; }
    }
}


