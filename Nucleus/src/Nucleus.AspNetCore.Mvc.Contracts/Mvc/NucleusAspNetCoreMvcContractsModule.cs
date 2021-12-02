using Nucleus.Application;
using Nucleus.Modularity;

namespace Nucleus.AspNetCore.Mvc
{
    [DependsOn(
        typeof(NucleusDddApplicationContractsModule)
        )]
    public class NucleusAspNetCoreMvcContractsModule : NucleusModule
    {

    }
}





