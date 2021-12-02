using Microsoft.Extensions.DependencyInjection;
using Nucleus.Data;
using Nucleus.EventBus.Abstractions;
using Nucleus.Modularity;
using Nucleus.MultiTenancy.ConfigurationStore;
using Nucleus.Security;

namespace Nucleus.MultiTenancy
{
    //TODO: Create a Nucleus.MultiTenancy.Abstractions package?

    [DependsOn(
        typeof(NucleusDataModule),
        typeof(NucleusSecurityModule),
        typeof(NucleusEventBusAbstractionsModule)
        )]
    public class NucleusMultiTenancyModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ICurrentTenantAccessor>(AsyncLocalCurrentTenantAccessor.Instance);
            
            var configuration = context.Services.GetConfiguration();
            Configure<NucleusDefaultTenantStoreOptions>(configuration);
        }
    }
}








