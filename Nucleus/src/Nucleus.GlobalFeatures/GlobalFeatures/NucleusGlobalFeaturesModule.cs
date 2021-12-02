using Microsoft.Extensions.DependencyInjection;
using Nucleus.Authorization;
using Nucleus.GlobalFeatures.Localization;
using Nucleus.Localization;
using Nucleus.Localization.ExceptionHandling;
using Nucleus.Modularity;
using Nucleus.VirtualFileSystem;

namespace Nucleus.GlobalFeatures
{
    [DependsOn(
        typeof(NucleusLocalizationModule),
        typeof(NucleusVirtualFileSystemModule),
        typeof(NucleusAuthorizationAbstractionsModule)
    )]
    public class NucleusGlobalFeaturesModule : NucleusModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.OnRegistred(GlobalFeatureInterceptorRegistrar.RegisterIfNeeded);
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<NucleusVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NucleusGlobalFeatureResource>();
            });

            Configure<NucleusLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<NucleusGlobalFeatureResource>("en")
                    .AddVirtualJson("/Nucleus/GlobalFeatures/Localization");
            });

            Configure<NucleusExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Nucleus.GlobalFeature", typeof(NucleusGlobalFeatureResource));
            });
        }
    }
}








