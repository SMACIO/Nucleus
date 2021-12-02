using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Nucleus.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionDynamicOptionsManagerExtensions
    {
        public static IServiceCollection AddNucleusDynamicOptions<TOptions, TManager>(this IServiceCollection services)
            where TOptions : class
            where TManager : NucleusDynamicOptionsManager<TOptions>
        {
            services.Replace(ServiceDescriptor.Scoped(typeof(IOptions<TOptions>), typeof(TManager)));
            services.Replace(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<TOptions>), typeof(TManager)));

            return services;
        }
    }
}



