using Microsoft.Extensions.DependencyInjection;
using Nucleus.Castle;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.Threading;
using Nucleus.Validation;
using Nucleus.ExceptionHandling;
using Nucleus.Http.Client.DynamicProxying;

namespace Nucleus.Http.Client
{
    [DependsOn(
        typeof(NucleusHttpModule),
        typeof(NucleusCastleCoreModule),
        typeof(NucleusThreadingModule),
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusValidationModule),
        typeof(NucleusExceptionHandlingModule)
        )]
    public class NucleusHttpClientModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            Configure<NucleusRemoteServiceOptions>(configuration);

            context.Services.AddTransient(typeof(DynamicHttpProxyInterceptorClientProxy<>));
        }
    }
}






