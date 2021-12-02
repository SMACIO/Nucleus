using Microsoft.Extensions.DependencyInjection;
using Nucleus.Data;
using Nucleus.Json;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.Security;
using Nucleus.Threading;
using Nucleus.Timing;

namespace Nucleus.Auditing
{
    [DependsOn(
        typeof(NucleusDataModule),
        typeof(NucleusJsonModule),
        typeof(NucleusTimingModule),
        typeof(NucleusSecurityModule),
        typeof(NucleusThreadingModule),
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusAuditingContractsModule)
        )]
    public class NucleusAuditingModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(AuditingInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}





