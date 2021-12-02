using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;

namespace Nucleus
{
    public static class NucleusApplicationFactory
    {
        public static INucleusApplicationWithInternalServiceProvider Create<TStartupModule>(
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction = null)
            where TStartupModule : INucleusModule
        {
            return Create(typeof(TStartupModule), optionsAction);
        }

        public static INucleusApplicationWithInternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction = null)
        {
            return new NucleusApplicationWithInternalServiceProvider(startupModuleType, optionsAction);
        }

        public static INucleusApplicationWithExternalServiceProvider Create<TStartupModule>(
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction = null)
            where TStartupModule : INucleusModule
        {
            return Create(typeof(TStartupModule), services, optionsAction);
        }

        public static INucleusApplicationWithExternalServiceProvider Create(
            [NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction = null)
        {
            return new NucleusApplicationWithExternalServiceProvider(startupModuleType, services, optionsAction);
        }
    }
}






