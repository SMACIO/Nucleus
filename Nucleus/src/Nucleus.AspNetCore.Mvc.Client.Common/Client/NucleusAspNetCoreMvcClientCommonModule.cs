using Microsoft.Extensions.DependencyInjection;
using Nucleus.Authorization;
using Nucleus.Caching;
using Nucleus.Features;
using Nucleus.Http.Client;
using Nucleus.Localization;
using Nucleus.Modularity;
using Nucleus.VirtualFileSystem;

namespace Nucleus.AspNetCore.Mvc.Client
{
    [DependsOn(
        typeof(NucleusHttpClientModule),
        typeof(NucleusAspNetCoreMvcContractsModule),
        typeof(NucleusCachingModule),
        typeof(NucleusLocalizationModule),
        typeof(NucleusAuthorizationModule),
        typeof(NucleusFeaturesModule),
        typeof(NucleusVirtualFileSystemModule)
    )]
    public class NucleusAspNetCoreMvcClientCommonModule : NucleusModule
    {
        public const string RemoteServiceName = "NucleusMvcClient";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(typeof(NucleusAspNetCoreMvcContractsModule).Assembly, RemoteServiceName);

            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusAspNetCoreMvcClientCommonModule>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.GlobalContributors.Add<RemoteLocalizationContributor>();
            });
        }
    }
}






