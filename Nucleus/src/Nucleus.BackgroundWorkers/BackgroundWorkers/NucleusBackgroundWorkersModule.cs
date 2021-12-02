using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nucleus.Modularity;
using Nucleus.Threading;

namespace Nucleus.BackgroundWorkers
{
    [DependsOn(
        typeof(NucleusThreadingModule)
        )]
    public class NucleusBackgroundWorkersModule : NucleusModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var options = context.ServiceProvider.GetRequiredService<IOptions<NucleusBackgroundWorkerOptions>>().Value;
            if (options.IsEnabled)
            {
                AsyncHelper.RunSync(
                    () => context.ServiceProvider
                        .GetRequiredService<IBackgroundWorkerManager>()
                        .StartAsync()
                );
            }
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            var options = context.ServiceProvider.GetRequiredService<IOptions<NucleusBackgroundWorkerOptions>>().Value;
            if (options.IsEnabled)
            {
                AsyncHelper.RunSync(
                    () => context.ServiceProvider
                        .GetRequiredService<IBackgroundWorkerManager>()
                        .StopAsync()
                );
            }
        }
    }
}






