using Microsoft.Extensions.DependencyInjection;
using Nucleus.Auditing;
using Nucleus.Data;
using Nucleus.Domain.Repositories;
using Nucleus.EventBus;
using Nucleus.ExceptionHandling;
using Nucleus.Guids;
using Nucleus.Modularity;
using Nucleus.MultiTenancy;
using Nucleus.ObjectMapping;
using Nucleus.Specifications;
using Nucleus.Threading;
using Nucleus.Timing;
using Nucleus.Uow;

namespace Nucleus.Domain
{
    [DependsOn(
        typeof(NucleusAuditingModule),
        typeof(NucleusDataModule),
        typeof(NucleusEventBusModule),
        typeof(NucleusGuidsModule),
        typeof(NucleusMultiTenancyModule),
        typeof(NucleusThreadingModule),
        typeof(NucleusTimingModule),
        typeof(NucleusUnitOfWorkModule),
        typeof(NucleusObjectMappingModule),
        typeof(NucleusExceptionHandlingModule),
        typeof(NucleusSpecificationsModule)
        )]
    public class NucleusDddDomainModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddConventionalRegistrar(new NucleusRepositoryConventionalRegistrar());
        }
    }
}
