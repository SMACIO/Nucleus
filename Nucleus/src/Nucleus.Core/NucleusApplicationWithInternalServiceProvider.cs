using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus
{
    internal class NucleusApplicationWithInternalServiceProvider : NucleusApplicationBase, INucleusApplicationWithInternalServiceProvider
    {
        public IServiceScope ServiceScope { get; private set; }

        public NucleusApplicationWithInternalServiceProvider(
            [NotNull] Type startupModuleType,
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction
            ) : this(
            startupModuleType,
            new ServiceCollection(),
            optionsAction)
        {

        }

        private NucleusApplicationWithInternalServiceProvider(
            [NotNull] Type startupModuleType, 
            [NotNull] IServiceCollection services, 
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction
            ) : base(
                startupModuleType, 
                services, 
                optionsAction)
        {
            Services.AddSingleton<INucleusApplicationWithInternalServiceProvider>(this);
        }

        public IServiceProvider CreateServiceProvider()
        {
            if (ServiceProvider != null)
            {
                return ServiceProvider;
            }
            
            ServiceScope = Services.BuildServiceProviderFromFactory().CreateScope();
            SetServiceProvider(ServiceScope.ServiceProvider);
            
            return ServiceProvider;
        }

        public void Initialize()
        {
            CreateServiceProvider();
            InitializeModules();
        }

        public override void Dispose()
        {
            base.Dispose();
            ServiceScope.Dispose();
        }
    }
}





