using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity.PlugIns;

namespace Nucleus.Modularity
{
    public interface IModuleLoader
    {
        [NotNull]
        INucleusModuleDescriptor[] LoadModules(
            [NotNull] IServiceCollection services,
            [NotNull] Type startupModuleType,
            [NotNull] PlugInSourceList plugInSources
        );
    }
}



