using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;

namespace Nucleus.Threading
{
    public class NucleusThreadingModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);
            context.Services.AddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
        }
    }
}




