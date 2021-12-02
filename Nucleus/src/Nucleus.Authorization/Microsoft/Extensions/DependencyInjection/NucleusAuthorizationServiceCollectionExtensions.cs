using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nucleus.Authorization;
using Nucleus.Authorization.Permissions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NucleusAuthorizationServiceCollectionExtensions
    {
        public static IServiceCollection AddAlwaysAllowAuthorization(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IAuthorizationService, AlwaysAllowAuthorizationService>());
            services.Replace(ServiceDescriptor.Singleton<INucleusAuthorizationService, AlwaysAllowAuthorizationService>());
            services.Replace(ServiceDescriptor.Singleton<IMethodInvocationAuthorizationService, AlwaysAllowMethodInvocationAuthorizationService>());
            return services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());
        }
    }
}



