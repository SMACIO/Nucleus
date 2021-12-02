using System.Collections.Generic;
using Nucleus.Application.Services;
using Nucleus.Authorization;
using Nucleus.Domain;
using Nucleus.Features;
using Nucleus.GlobalFeatures;
using Nucleus.Http;
using Nucleus.Http.Modeling;
using Nucleus.Modularity;
using Nucleus.ObjectMapping;
using Nucleus.Security;
using Nucleus.Settings;
using Nucleus.Uow;
using Nucleus.Validation;

namespace Nucleus.Application
{
    [DependsOn(
        typeof(NucleusDddDomainModule),
        typeof(NucleusDddApplicationContractsModule),
        typeof(NucleusSecurityModule),
        typeof(NucleusObjectMappingModule),
        typeof(NucleusValidationModule),
        typeof(NucleusAuthorizationModule),
        typeof(NucleusHttpAbstractionsModule),
        typeof(NucleusSettingsModule),
        typeof(NucleusFeaturesModule),
        typeof(NucleusGlobalFeaturesModule)
        )]
    public class NucleusDddApplicationModule : NucleusModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<NucleusApiDescriptionModelOptions>(options =>
            {
                //TODO: Should we move related items to their own projects?
                options.IgnoredInterfaces.AddIfNotContains(typeof(IRemoteService));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IApplicationService));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IUnitOfWorkEnabled));
            });
        }
    }
}






