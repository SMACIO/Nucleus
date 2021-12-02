using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus
{
    internal class NucleusApplicationWithExternalServiceProvider : NucleusApplicationBase, INucleusApplicationWithExternalServiceProvider
    {
        public NucleusApplicationWithExternalServiceProvider(
            [NotNull] Type startupModuleType, 
            [NotNull] IServiceCollection services, 
            [CanBeNull] Action<NucleusApplicationCreationOptions> optionsAction
            ) : base(
                startupModuleType, 
                services, 
                optionsAction)
        {
            services.AddSingleton<INucleusApplicationWithExternalServiceProvider>(this);
        }

        void INucleusApplicationWithExternalServiceProvider.SetServiceProvider([NotNull] IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            
            if (ServiceProvider != null)
            {
                if (ServiceProvider != serviceProvider)
                {
                    throw new NucleusException("Service provider was already set before to another service provider instance.");
                }
                
                return;
            }
            
            SetServiceProvider(serviceProvider);
        }

        public void Initialize([NotNull] IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            SetServiceProvider(serviceProvider);

            InitializeModules();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (ServiceProvider is IDisposable disposableServiceProvider)
            {
                disposableServiceProvider.Dispose();
            }
        }
    }
}







