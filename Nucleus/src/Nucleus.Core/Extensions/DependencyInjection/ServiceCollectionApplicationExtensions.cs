using System;
using JetBrains.Annotations;
using Nucleus;
using Nucleus.Modularity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static INucleusApplicationWithExternalServiceProvider AddApplication<TStartupModule>(
            [NotNull] this IServiceCollection services, 
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction = null)
            where TStartupModule : INucleusModule
        {
            return NucleusApplicationFactory.Create<TStartupModule>(services, optionsAction);
        }

        public static INucleusApplicationWithExternalServiceProvider AddApplication(
            [NotNull] this IServiceCollection services,
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction = null)
        {
            return NucleusApplicationFactory.Create(startupModuleType, services, optionsAction);
        }
    }
}




