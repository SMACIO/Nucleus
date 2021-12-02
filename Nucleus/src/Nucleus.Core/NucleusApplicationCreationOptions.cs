using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity.PlugIns;

namespace Nucleus
{
    public class NucleusApplicationCreationOptions
    {
        [NotNull]
        public IServiceCollection Services { get; }

        [NotNull]
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// The options in this property only take effect when IConfiguration not registered.
        /// </summary>
        [NotNull]
        public NucleusConfigurationBuilderOptions Configuration {get; }

        public NucleusApplicationCreationOptions([NotNull] IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            PlugInSources = new PlugInSourceList();
            Configuration = new NucleusConfigurationBuilderOptions();
        }
    }
}





