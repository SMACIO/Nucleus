using Microsoft.Extensions.DependencyInjection;
using Nucleus.Modularity;

namespace Nucleus.Uow
{
    public class NucleusUnitOfWorkModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(UnitOfWorkInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}




