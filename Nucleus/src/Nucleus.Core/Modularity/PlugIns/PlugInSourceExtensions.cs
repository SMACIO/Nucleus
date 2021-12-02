using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Nucleus.Logging;

namespace Nucleus.Modularity.PlugIns
{
    public static class PlugInSourceExtensions
    {
        [NotNull]
        public static Type[] GetModulesWithAllDependencies([NotNull] this IPlugInSource plugInSource, ILogger logger)
        {
            Check.NotNull(plugInSource, nameof(plugInSource));

            return plugInSource
                .GetModules()
                .SelectMany(type => NucleusModuleHelper.FindAllModuleTypes(type, logger))
                .Distinct()
                .ToArray();
        }
    }
}



