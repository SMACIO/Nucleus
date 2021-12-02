using Microsoft.Extensions.DependencyInjection;
using Nucleus.Castle.DynamicProxy;
using Nucleus.Modularity;

namespace Nucleus.Castle
{
    public class NucleusCastleCoreModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient(typeof(NucleusAsyncDeterminationInterceptor<>));
        }
    }
}





