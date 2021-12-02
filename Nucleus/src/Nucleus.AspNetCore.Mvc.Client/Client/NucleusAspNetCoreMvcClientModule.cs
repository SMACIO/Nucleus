using Nucleus.EventBus;
using Nucleus.Modularity;

namespace Nucleus.AspNetCore.Mvc.Client
{
    [DependsOn(
        typeof(NucleusAspNetCoreMvcClientCommonModule),
        typeof(NucleusEventBusModule)
        )]
    public class NucleusAspNetCoreMvcClientModule : NucleusModule
    {
    }
}





